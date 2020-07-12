using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropme : MonoBehaviour
{
    void OnCollisionEnter(Collision other){
        if(other.collider.CompareTag("Player") || other.collider.CompareTag("Environment")){
            //if we have a parent, we're probably being held?
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.transform.parent.transform.DetachChildren();
        }
    }
}
