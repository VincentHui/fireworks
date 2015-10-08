using UnityEngine;
using System.Collections;

public class RayCastMenu : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    bool hasHitBillboard;
    bool lastFrameHitBillboard;

    public GameObject creditsTextObject;
    public GameObject creditsTextObject1;
    

    void Start()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        creditsTextObject.SetActive(false);
    }

    void Update()
    {
        //if (Physics.Raycast(ray, out hit))
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10.0f))
        {
            if (hit.collider.name == "Game 1")
            {
                iTween.PunchScale(hit.collider.gameObject, new Vector3(0.15f, 0.15f, 0.15f), 2.0f);
            }
            else if (hit.collider.name == "Game 2")
            {
                iTween.PunchScale(hit.collider.gameObject, new Vector3(0.15f, 0.15f, 0.15f), 2.0f);
            }
            else if (hit.collider.name == "Plane (2)") //Billboard
            {
                //Activates credits text when player looks at the billboard
                creditsTextObject.SetActive(true);
                creditsTextObject1.SetActive(false);
            }

            if (hit.collider.name != "Plane (2)")
            {
                //Turns credit text off when looking away from billboard
                creditsTextObject.SetActive(false);
                creditsTextObject1.SetActive(true);

                //Player is not viewing the billboard this frame
                hasHitBillboard = false;
            }
        }
        else if(!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10.0f))
        {
            //Turns credit text off when looking away from billboard
            creditsTextObject.SetActive(false);
            creditsTextObject1.SetActive(true);

            //Player is not viewing the billboard this frame
            hasHitBillboard = false;
        }

        //Checks if the player, from viewing the billboard, looks away
        if (lastFrameHitBillboard && hasHitBillboard == false)
        {
            //Turns credit text off when looking away from billboard
            creditsTextObject.SetActive(false);

            //player no longer viewing billboard on lst frame
            lastFrameHitBillboard = false;
        }
    }
}
