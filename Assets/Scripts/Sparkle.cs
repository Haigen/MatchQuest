using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sparkle : MonoBehaviour
{

    public void SparkleAnim()
    {
        StartCoroutine(SparkleGo());
    }
    
    IEnumerator SparkleGo()
    {
        GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(1f);
        GetComponentInChildren<ParticleSystem>().Stop();
    }
}
