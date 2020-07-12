using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXobjects : MonoBehaviour
{
    public AK.Wwise.Event playSFX;
    public AK.Wwise.Event stopSFX;
    public GameObject hands;
    private hand_controller currentObject;
    bool holding = false;

    // Start is called before the first frame update
    void Start()
    {
        currentObject = hands.GetComponent<hand_controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if(holding == false)
        {
            if (currentObject != null && gameObject.layer == LayerMask.NameToLayer("Grabbed Object"))
            {
                holding = true;
                playSFX.Post(gameObject);
            }
        }
        else
        {
            holding = false;
            stopSFX.Post(gameObject);
        }

        //if (hands.transform.childCount == 0 && gameObject.layer == LayerMask.NameToLayer("Grabbed Object"))
        //{
        //    playSFX.Post(gameObject);
        //}
    }
}
