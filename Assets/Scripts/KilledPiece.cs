using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KilledPiece : MonoBehaviour
{
    public bool falling;

    float speed = 16f;
    float gravity = 32f;
    Vector2 moveDir;
    RectTransform rect;
    Image img;

    public void Initialize(Sprite piece, Vector2 start, Vector2 size)
    {
        img = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        img.sprite = piece;
        rect.anchoredPosition = start;
        rect.sizeDelta = new Vector2(size.x, size.y);
        falling = true;

        moveDir = Vector2.up;
        moveDir.x = Random.Range(-1.0f, 1.0f);
        moveDir *= speed / 2;

    }

    // Update is called once per frame
    void Update()
    {
        if (!falling) return;

        moveDir.y -= Time.deltaTime * gravity;
        moveDir.x = Mathf.Lerp(moveDir.x, 0, Time.deltaTime);
        
        //Scale with fall
        //rect.sizeDelta -= new Vector2(Time.deltaTime * gravity,Time.deltaTime * gravity );
        
        
        rect.anchoredPosition += moveDir * Time.deltaTime * speed;


        if (rect.position.x < -rect.rect.width || rect.position.x > Screen.width + rect.rect.width || rect.position.y < -rect.rect.width || rect.position.y > Screen.height + rect.rect.width)
            falling = false;

    }
}