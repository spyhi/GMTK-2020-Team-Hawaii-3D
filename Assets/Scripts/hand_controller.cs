using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//IMPORTANT: ALL GRABBABLE OBJECTS (and their children) MUST HAVE TAG "CanGrab" AND BE ON LAYER "InteractableObject"

public class hand_controller : MonoBehaviour
{
    public Collider target;
    void Update(){
        if (target != null)
        {
            //print(target.gameObject.name);
        }
        if (Input.GetButtonDown("Fire1")){
            if (gameObject.transform.childCount == 0){
                if(target.attachedRigidbody.CompareTag("CanGrab"))
                {
                    target.attachedRigidbody.transform.SetParent(gameObject.transform);
                    target.attachedRigidbody.transform.localPosition = new Vector3(0, 0, 0);
                    SetAllToLayer(target.attachedRigidbody.transform, 8, 11); // Find parent rigidbody and set all children to layer 8: grabbed object
                    target.attachedRigidbody.isKinematic = true;
                    Debug.Log("Grabbed " + target.attachedRigidbody.name);
                }
            }
            else{
                Rigidbody grabbedObject = gameObject.transform.GetChild(0).GetComponent<Rigidbody>();
                grabbedObject.isKinematic = false;
                SetAllToLayer(grabbedObject.transform, 11, 8); // 11: interactable / grabbable objects

                gameObject.transform.DetachChildren();
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        target = other;
    }
    void OnTriggerExit(Collider other){
        target = null;
    }

    void SetAllToLayer(Transform obj, int layer, int from) //recursive function to set all children of object to given layer no. (quick fix)
    {
        if (obj.gameObject.layer == from) { obj.gameObject.layer = layer; }

        for (int i = 0; i < obj.childCount; i++)
        {
            SetAllToLayer(obj.GetChild(i), layer, from);
        }
    }
}
