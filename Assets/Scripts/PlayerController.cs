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
                        Debug.Log("Something Hit");
                        if (raycastHit.collider.name == "Soccer")
                        {
                            Debug.Log("Soccer Ball clicked");
                        }

                        //OR with Tag

                        if (raycastHit.collider.CompareTag("SoccerTag"))
                        {
                            Debug.Log("Soccer Ball clicked");
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
