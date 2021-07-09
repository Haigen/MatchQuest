using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int gridX = 7;
    public int gridY = 13;
    public GameObject tPrefab;
    public GameObject canvas;
    [Serializable] public class tileInfo
    {
        public Sprite tSprite;
        public string tID;
        public bool isCleared;
    }
    public List<tileInfo> tTiles;
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
        
        tileInfo[] previousLeft = new tileInfo[gridY];
        tileInfo previousBelow = new tileInfo();
        
        for (int x = 0; x < gridX; x++) 
        {
            for (int y = 0; y < gridY; y++) 
            {
                GameObject newTileObj = Instantiate(tPrefab, new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), tPrefab.transform.rotation);
                newTileObj.name = "Tile: " + x + "," + y;
                tiles[x, y] = newTileObj;
                List<tileInfo> possibleTiles = new List<tileInfo>();
                possibleTiles.AddRange(tTiles);
                possibleTiles.Remove(previousLeft[y]);
                possibleTiles.Remove(previousBelow);
                tileInfo newTile = possibleTiles[UnityEngine.Random.Range(0, possibleTiles.Count)];
                newTileObj.GetComponent<Tile>().myInfo = newTile;
                newTileObj.GetComponent<SpriteRenderer>().sprite = newTileObj.GetComponent<Tile>().myInfo.tSprite;
                previousLeft[y] = newTile;
                previousBelow = newTile;
            }
        }
    }
    
    public IEnumerator FindNullTiles() 
    {
        for (int x = 0; x < gridX; x++) 
        {
            for (int y = 0; y < gridY; y++) 
            {
                if (tiles[x, y].GetComponent<Tile>().myInfo.isCleared) 
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
        List<GameObject>  tilesToShift = new List<GameObject>();
        int nullCount = 0;

        for (int y = yStart; y < gridY; y++) 
        {
            GameObject tileToShift = tiles[x, y];
            if (tileToShift.GetComponent<Tile>().myInfo.isCleared) 
            {
                nullCount++;
            }
            tilesToShift.Add(tileToShift);
        }

        for (int i = 0; i < nullCount; i++) 
        {
            yield return new WaitForSeconds(shiftDelay);
            for (int k = 0; k < tilesToShift.Count - 1; k++) 
            {
                tilesToShift[k].GetComponent<Tile>().myInfo = tilesToShift[k + 1].GetComponent<Tile>().myInfo;
                tilesToShift[k].GetComponent<Tile>().UpdateGraphics();
                tilesToShift[k + 1].GetComponent<Tile>().myInfo = GetNewTile(x, gridY - 1);
                tilesToShift[k + 1].GetComponent<Tile>().UpdateGraphics();
            }
        }
        isShifting = false;
    }
    
    private tileInfo GetNewTile(int x, int y) 
    {
        List<tileInfo> possibleTiles = new List<tileInfo>();
        possibleTiles.AddRange(tTiles);

        if (x > 0) 
        {
            possibleTiles.Remove(tiles[x - 1, y].GetComponent<Tile>().myInfo);
        }
        if (x < gridX - 1) 
        {
            possibleTiles.Remove(tiles[x + 1, y].GetComponent<Tile>().myInfo);
        }
        if (y > 0) 
        {
            possibleTiles.Remove(tiles[x, y - 1].GetComponent<Tile>().myInfo);
        }

        return possibleTiles[UnityEngine.Random.Range(0, possibleTiles.Count)];
    }
}
