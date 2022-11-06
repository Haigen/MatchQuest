using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldWindowGroup : MonoBehaviour
{
    public GameObject worldManager;
    public List<GameObject> worlds;

    public void OpenWorldWindow(int worldIndex)
    {
        worldManager.SetActive(true);
        for (int i = 0; i < worlds.Count; i++)
        {
            if (i == worldIndex)
            {
                worlds[i].SetActive(true);
            }
            else
            {
                worlds[i].SetActive(false);
            }
        }
        gameObject.SetActive(false);
    }
}
