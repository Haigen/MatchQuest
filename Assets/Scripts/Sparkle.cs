using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sparkle : MonoBehaviour
{

    public void Start()
    {
        StartCoroutine(SparkleGo());
    }
    
    IEnumerator SparkleGo()
    {
        
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
