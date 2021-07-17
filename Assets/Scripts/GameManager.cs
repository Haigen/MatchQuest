using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<WorldObjective> worldObjectives;

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenLevel()
    {
        SceneManager.LoadScene(1);
    }
}
