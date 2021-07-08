using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int gridX = 7;
    public int gridY = 13;
    public GameObject tPrefab;
    public GameObject canvas;
    public List<Sprite> tSprites = new List<Sprite>();
    public bool isShifting;
    public GameObject[,] tiles;
    public int highlightedTileCount;

    void Awake()
    {
        tiles = new GameObject[gridX, gridY];
    }

    void Start()
    {
        instance = GetComponent<GameManager>();
        Vector2 offset = tPrefab.GetComponent<SpriteRenderer>().bounds.size;
        CreateBoard(offset.x, offset.y);
    }

    void CreateBoard(float xOffset, float yOffset)
    {
        tiles = new GameObject[gridX, gridY];

        float startX = transform.position.x;
        float startY = transform.position.y;
        
        Sprite[] previousLeft = new Sprite[gridY];
        Sprite previousBelow = null;
        
        for (int x = 0; x < gridX; x++) 
        {
            for (int y = 0; y < gridY; y++) 
            {
                GameObject newTile = Instantiate(tPrefab, new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), tPrefab.transform.rotation);
                newTile.name = "Tile: " + x + "," + y;
                tiles[x, y] = newTile;
                List<Sprite> possibleSprites = new List<Sprite>();
                possibleSprites.AddRange(tSprites);
                possibleSprites.Remove(previousLeft[y]);
                possibleSprites.Remove(previousBelow);
                Sprite newSprite = possibleSprites[Random.Range(0, possibleSprites.Count)];
                newTile.GetComponent<SpriteRenderer>().sprite = newSprite;
                previousLeft[y] = newSprite;
                previousBelow = newSprite;
            }
        }
    }
    
    public IEnumerator FindNullTiles() 
    {
        for (int x = 0; x < gridX; x++) 
        {
            for (int y = 0; y < gridY; y++) 
            {
                if (tiles[x, y].GetComponent<SpriteRenderer>().sprite == null) 
                {
                    yield return StartCoroutine(ShiftTilesDown(x, y));
                    break;
                }
            }
        }
        //Combo system
        for (int x = 0; x < gridX; x++) 
        {
            for (int y = 0; y < gridY; y++) 
            {
                tiles[x, y].GetComponent<Tile>().ClearAllMatches();
            }
        }
    }
    
    private IEnumerator ShiftTilesDown(int x, int yStart, float shiftDelay = .05f) 
    {
        isShifting = true;
        List<SpriteRenderer>  renders = new List<SpriteRenderer>();
        int nullCount = 0;

        for (int y = yStart; y < gridY; y++) 
        {  // 1
            SpriteRenderer render = tiles[x, y].GetComponent<SpriteRenderer>();
            if (render.sprite == null) 
            {
                nullCount++;
            }
            renders.Add(render);
        }

        for (int i = 0; i < nullCount; i++) 
        {
            yield return new WaitForSeconds(shiftDelay);
            for (int k = 0; k < renders.Count - 1; k++) 
            {
                renders[k].sprite = renders[k + 1].sprite;
                renders[k + 1].sprite = GetNewSprite(x, gridY - 1);
            }
        }
        isShifting = false;
    }
    
    private Sprite GetNewSprite(int x, int y) 
    {
        List<Sprite> possibleCharacters = new List<Sprite>();
        possibleCharacters.AddRange(tSprites);

        if (x > 0) 
        {
            possibleCharacters.Remove(tiles[x - 1, y].GetComponent<SpriteRenderer>().sprite);
        }
        if (x < gridX - 1) 
        {
            possibleCharacters.Remove(tiles[x + 1, y].GetComponent<SpriteRenderer>().sprite);
        }
        if (y > 0) 
        {
            possibleCharacters.Remove(tiles[x, y - 1].GetComponent<SpriteRenderer>().sprite);
        }

        return possibleCharacters[Random.Range(0, possibleCharacters.Count)];
    }
}
