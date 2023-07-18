using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldData : MonoBehaviour
{
    public LevelData[] levels;
    public Sprite[] bgs;
    public GameObject bg;

    public ArrayLayout LoadBoard(int lvlId)
    {
        ArrayLayout lvlBoard = new ArrayLayout();
        if (levels[lvlId - 1] != null)
        {
            lvlBoard = levels[lvlId - 1].GetComponent<LevelData>().boardLayout;
            LoadBg(lvlId);
            return lvlBoard;
        }

        return null;
    }
    
    public Sprite[] LoadPieces(int lvlId)
    {
        
        
        if (levels[lvlId - 1] != null)
        {
            Sprite[] pieces = new Sprite[levels[lvlId - 1].pieces.Length];
            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i] = levels[lvlId - 1].pieces[i];
            }
            return pieces;
        }

        return null;
    }

    public void LoadBg(int lvlId)
    {
        if (lvlId <= 10)
        {
            bg.GetComponent<Image>().sprite = bgs[0];
        }
        else if (lvlId <= 20)
        {
            bg.GetComponent<Image>().sprite = bgs[1];
        }
        else if (lvlId <= 30)
        {
            bg.GetComponent<Image>().sprite = bgs[2];
        }
        else if (lvlId <= 40)
        {
            bg.GetComponent<Image>().sprite = bgs[3];
        }
        else if (lvlId <= 50)
        {
            bg.GetComponent<Image>().sprite = bgs[4];
        }
    }
}
