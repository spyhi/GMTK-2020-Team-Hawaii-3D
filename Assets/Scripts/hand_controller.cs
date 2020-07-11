using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class hand_controller : MonoBehaviour
{
    public Collider target;
    void Update(){
        if(Input.GetButtonDown("Fire1")){
            if(gameObject.transform.childCount == 0){
                if(target.CompareTag("CanGrab")){
                    target.transform.SetParent(gameObject.transform);
                    target.transform.localPosition = new Vector3(0, 0, 0);
                    target.GetComponent<Rigidbody>().isKinematic = true;
                    Debug.Log("Grabbed" + target.name);
                }
            }
            else{
                gameObject.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
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
}
