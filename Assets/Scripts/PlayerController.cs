using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
                {
                    Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit raycastHit;
                    if (Physics.Raycast(raycast, out raycastHit))
                    {
                        if (raycastHit.collider.CompareTag("Tile"))
                        {
                            raycastHit.collider.GetComponent<Tile>().Interact();
                        }
                    }
                }

                break;
            default:
            case RuntimePlatform.WindowsEditor:
                if (Input.GetMouseButtonDown(0))
                {
                    Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit raycastHit;
                    if (Physics.Raycast(raycast, out raycastHit))
                    {
                        if (raycastHit.collider.CompareTag("Tile"))
                        {
                            raycastHit.collider.GetComponent<Tile>().Interact();
                        }
                    }
                }
                break;

        }
    }
}
