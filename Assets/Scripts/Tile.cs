using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
    private static Tile previousSelected = null;

    public GameManager.tileInfo myInfo;
    private SpriteRenderer render;
    private GameManager gm;
    private bool isSelected = false;
    private bool matchFound = false;

    private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    void Awake() 
    {
        render = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void UpdateGraphics()
    {
        render.sprite = myInfo.tSprite;
    }

    private void Select() 
    {
        isSelected = true;
        render.color = selectedColor;
        previousSelected = gameObject.GetComponent<Tile>();
        //SFXManager.instance.PlaySFX(Clip.Select);
    }

    private void Deselect() 
    {
        isSelected = false;
        render.color = Color.white;
        previousSelected = null;
    }

    void OnMouseDown()
    {
        Interact();
    }

    public void Interact() 
    {
        if (myInfo.isCleared || gm.isShifting) 
        {
            return;
        }

        if (isSelected) 
        {
            Deselect();
        } 
        else 
        {
            if (previousSelected == null) 
            {
                Select();
            } 
            else 
            {
                if (GetAllAdjacentTiles().Contains(previousSelected.gameObject)) 
                {
                    SwapInfo(previousSelected);
                    previousSelected.UpdateGraphics();
                    UpdateGraphics();
                    previousSelected.ClearAllMatches();
                    previousSelected.Deselect();
                    ClearAllMatches();
                } 
                else 
                {
                    previousSelected.GetComponent<Tile>().Deselect();
                    Select();
                }
            }
        }
    }
    
    public void SwapInfo(Tile prevTile) 
    {
        if (myInfo.tID == prevTile.myInfo.tID) 
        {
            return;
        }

        GameManager.tileInfo tempInfo = prevTile.myInfo;
        prevTile.myInfo = myInfo;
        myInfo = tempInfo;
        //SFXManager.instance.PlaySFX(Clip.Swap);
    }
    private GameObject GetAdjacent(Vector2 castDir) 
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
        if (hit.collider != null) 
        {
            return hit.collider.gameObject;
        }
        return null;
    }
    private List<GameObject> GetAllAdjacentTiles()
    {
        List<GameObject> adjacentTiles = new List<GameObject>();
        for (int i = 0; i < adjacentDirections.Length; i++) 
        {
            adjacentTiles.Add(GetAdjacent(adjacentDirections[i]));
        }
        return adjacentTiles;
    }
    
    private List<GameObject> FindMatch(Vector2 castDir) 
    {
        List<GameObject> matchingTiles = new List<GameObject>();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
        while (hit.collider != null && hit.collider.GetComponent<Tile>().myInfo.tID == myInfo.tID) 
        {
            matchingTiles.Add(hit.collider.gameObject);
            hit = Physics2D.Raycast(hit.collider.transform.position, castDir);
        }
        return matchingTiles;
    }
    
    private void ClearMatch(Vector2[] paths)
    {
        List<GameObject> matchingTiles = new List<GameObject>();
        for (int i = 0; i < paths.Length; i++)
        {
            matchingTiles.AddRange(FindMatch(paths[i]));
        }
        if (matchingTiles.Count >= 2)
        {
            for (int i = 0; i < matchingTiles.Count; i++)
            {
                matchingTiles[i].GetComponent<Tile>().myInfo.isCleared = true;
            }
            matchFound = true;
        }
    }

    public void ClearAllMatches() 
    {
        if (myInfo.isCleared)
            return;

        ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
        ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });
        if (matchFound) 
        {
            myInfo.isCleared = true;
            matchFound = false;
            StopCoroutine(GameManager.instance.FindNullTiles());
            StartCoroutine(GameManager.instance.FindNullTiles());
            //SFXManager.instance.PlaySFX(Clip.Clear);
        }
    }


}
