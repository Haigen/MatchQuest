using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class KilledPiece : MonoBehaviour
{
    public bool falling;
    public bool finishedFalling = true;
    public int spacingId;

    public AnimationCurve speedCurve;
    public AnimationCurve bounceCurve;
    
    Vector2 moveDir;
    RectTransform rect;
    Image img;
    public RectTransform target;
    public Tween moveTween;
    public Tween scaleTween;
    public GameObject sparkleObj;

    public void Initialize(Sprite piece, Vector2 start, Vector2 size)
    {
        finishedFalling = false;
        target = GameObject.FindGameObjectWithTag("Destination").GetComponent<RectTransform>();
        img = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        img.sprite = piece;
        rect.anchoredPosition = start;
        rect.sizeDelta = new Vector2(size.x, size.y);
        falling = true;
        Instantiate(sparkleObj, transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (falling)
        {
            transform.GetComponent<Image>().enabled = true;
            falling = false;
            StartCoroutine(InitialDelay());
        }
    }

    IEnumerator InitialDelay()
    {
        float delay = (float)spacingId * 0.05f;
        yield return new WaitForSeconds(delay);
        StartCoroutine(MoveTo(1f));
    }
    IEnumerator MoveTo(float duration)
    {
        moveTween = rect.DOAnchorPos(target.anchoredPosition, duration, false).SetEase(speedCurve);
        scaleTween = rect.DOSizeDelta(new Vector2(rect.sizeDelta.x * 0.5f, rect.sizeDelta.y * 0.5f), duration, false).SetEase(bounceCurve);
        yield return new WaitForSeconds(duration);
        transform.GetComponent<Image>().enabled = false;
        finishedFalling = true;
    }
}