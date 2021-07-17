using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScrollingBG : MonoBehaviour
{

    public Vector2 startPos;

    public Vector2 destination;

    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RectTransform>().DOLocalMove(destination, duration, false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
