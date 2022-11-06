using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldWindow : MonoBehaviour
{
    public GameObject WorldSelector;

    public void CloseWindow()
    {
        WorldSelector.SetActive(true);
        gameObject.SetActive(false);
    }
}
