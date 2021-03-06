﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//IMPORTANT: ALL GRABBABLE OBJECTS (and their children) MUST HAVE TAG "CanGrab" AND BE ON LAYER "InteractableObject"

public class hand_controller : MonoBehaviour
{
    public Collider target;
    public GameObject noObject; //"null object", but it has a name parameter, so no need to try/catch or check null
    public GameObject currentHeldObject;
    bool safeToDrop;

    private void Start()
    {
        currentHeldObject = noObject;
    }

    void Update()
    {
        if (target != null)
        {
            //print(target.gameObject.name);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (gameObject.transform.childCount == 0)
            {
                if (target.attachedRigidbody.CompareTag("CanGrab"))
                {
                    if (target.attachedRigidbody.transform.Find("Fire(Clone)") == null)
                    {
                        Pickup(target.attachedRigidbody.gameObject);
                    }
                    else
                    {
                        print("Ouch your fingies");
                    }
                }
            }
            else
            {
                DropHeld();
            }
        }
        safeToDrop = false;
    }
    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Environment"))
        {
            target = other;
        } else
        {
            safeToDrop = true;
        }

    }
    void OnTriggerExit(Collider other)
    {
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

    public void DropHeld()
    {
        if (gameObject.transform.childCount != 0)
        {
            currentHeldObject = noObject;
            Rigidbody grabbedObject = gameObject.transform.GetChild(0).GetComponent<Rigidbody>();
            grabbedObject.isKinematic = false;
            SetAllToLayer(grabbedObject.transform, 11, 8); // 11: interactable / grabbable objects
            Debug.Log("LET GO: " + grabbedObject);
            //fly straight and true
            grabbedObject.rotation = gameObject.GetComponentInParent<Rigidbody>().rotation;
            if (!safeToDrop) { grabbedObject.AddForce(this.transform.forward * 1000); };
            gameObject.transform.DetachChildren();
        }

    }

    public void Pickup(GameObject object1)
    {
        currentHeldObject = object1;
        DropHeld();
        object1.transform.SetParent(gameObject.transform);
        object1.transform.localPosition = new Vector3(0, 0, 0);
        SetAllToLayer(object1.transform, 8, 11); // Find parent rigidbody and set all children to layer 8: grabbed object
        object1.GetComponent<Rigidbody>().isKinematic = true;
        Debug.Log("Grabbed " + object1.name);
    }
}