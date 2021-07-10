using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Chest : MonoBehaviour
{
    public List<Sprite> icons;
    public Image curImage;
    public enum tiers
    {
        wood_empty,
        wood_fill,
        wood_flow,
        stone_fill,
        stone_flow,
        gold_fill,
        gold_flow
    }

    public tiers myTier;
    public int gemCount;

    private bool isWobbling;

    void Start()
    {
        
    }
    
    

    // Update is called once per frame
    void Update()
    {
        switch (myTier)
        {
            case tiers.wood_empty:
                curImage.sprite = icons[0];
                break;
            case tiers.wood_fill:
                curImage.sprite = icons[1];
                break;
            case tiers.wood_flow:
                curImage.sprite = icons[2];
                break;
            case tiers.stone_fill:
                curImage.sprite = icons[3];
                break;
            case tiers.stone_flow:
                curImage.sprite = icons[4];
                break;
            case tiers.gold_fill:
                curImage.sprite = icons[5];
                break;
            case tiers.gold_flow:
                curImage.sprite = icons[6];
                break;
        }
        
    }

    public void AddGems(int amount)
    {
        
        if (!isWobbling)
        {
            isWobbling = true;
            StartCoroutine(WobbleAnim(1.0f));
        }


        gemCount += amount;
        if (gemCount > 20)
        {
            myTier = tiers.wood_fill;
            if (gemCount > 30)
            {
                myTier = tiers.wood_flow;
                if (gemCount > 40)
                {;
                    myTier = tiers.stone_fill;
                    if (gemCount > 50)
                    {
                        myTier = tiers.stone_flow;
                        if (gemCount > 60)
                        {
                            myTier = tiers.gold_fill;
                            if (gemCount > 70)
                            {
                                myTier = tiers.gold_flow;
                            }
                        }
                    }
                }
            }
        }
    }

    IEnumerator WobbleAnim(float duration)
    {
        transform.SetSiblingIndex(1);
        yield return new WaitForSeconds(duration);
        GetComponent<RectTransform>().DOPunchScale(new Vector3(1.01f, 1.01f, 1f), duration, 10, 0.1f);
        transform.SetAsLastSibling();
        isWobbling = false;
    }
}
