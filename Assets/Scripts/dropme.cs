using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropme : MonoBehaviour
{

    private void Update()
    {
        if (this.transform.position.y < -10)
        {
            this.transform.position = new Vector3(0, 0, 0);
        }
    }
    void OnCollisionEnter(Collision other){
        /**
        if((other.collider.CompareTag("Player") || other.collider.CompareTag("Environment")) && this.gameObject.layer == 8 )
        {//if object is on layer 8 ("grabbed") it is beind held.
            print("DROPME: " + other.collider.tag);
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            SetAllToLayer(gameObject.transform, 11, 8); // 11: interactable / grabbable objects
            gameObject.transform.parent.transform.DetachChildren();
        }**/
    }

    //shamelessly copied from hand_controller.cs
    void SetAllToLayer(Transform obj, int layer, int from) //recursive function to set all children of object to given layer no. (quick fix)
    {
        if (obj.gameObject.layer == from) { obj.gameObject.layer = layer; }

        for (int i = 0; i < obj.childCount; i++)
        {
            SetAllToLayer(obj.GetChild(i), layer, from);
        }
    }
}
