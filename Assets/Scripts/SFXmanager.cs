using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXmanager : MonoBehaviour
{
    public List<AK.Wwise.Event> PlayerInteraction = new List<AK.Wwise.Event>();
    public AK.Wwise.Event PickupItem;
    public AK.Wwise.Event DropItem;

    private hand_controller hands;

    // Update is called once per frame
    void Start()
    {
        hands = GetComponent<hand_controller>();

    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && gameObject.transform.childCount == 0 && hands.target.attachedRigidbody.CompareTag("CanGrab") && hands.target.attachedRigidbody.transform.Find("Fire(Clone)") == null)
        {
            PickupItem.Post(gameObject);
        }

    }

}
