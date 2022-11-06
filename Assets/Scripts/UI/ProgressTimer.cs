using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTimer : MonoBehaviour
{

    private Slider slider;

    public bool depleted;
    public float decreaseSpeed = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    
    public void DecrementProgress(float decrement)
    {
        slider.value -= decrement * Time.deltaTime * decreaseSpeed;
        if (slider.value <= 0)
        {
            depleted = true;
        }
    }
    public void IncrementProgress(float increment)
    {
        slider.value += increment;
    }
}
