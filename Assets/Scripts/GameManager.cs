using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int curLevel = 1;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    
    
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenLevel(int levelID)
    {
        curLevel = levelID;
        SceneManager.LoadScene(1);
        
    }
}
