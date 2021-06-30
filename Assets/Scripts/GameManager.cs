using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int gridX = 7;
    public int gridY = 13;
    public GameObject tPrefab;
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
                tiles[x, y] = newTile;
                newTile.transform.parent = transform;
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

}
