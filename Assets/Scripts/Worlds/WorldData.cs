using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldData : MonoBehaviour
{
    public LevelData[] levels;

    public ArrayLayout LoadBoard(int lvlId)
    {
        ArrayLayout lvlBoard = new ArrayLayout();
        if (levels[lvlId - 1] != null)
        {
            lvlBoard = levels[lvlId - 1].GetComponent<LevelData>().boardLayout;
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
}
