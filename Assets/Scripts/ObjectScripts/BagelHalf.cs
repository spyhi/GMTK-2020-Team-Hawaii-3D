using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagelHalf : MonoBehaviour
{
    GameObject toasterSlotOccupied = null;
    public Bagels bagelscript;
    public hand_controller handscript;

    int BagelEntryBufferFrames = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (toasterSlotOccupied != null && this.transform.parent != toasterSlotOccupied.transform)
        {
            print("Exit detected");
            ForceExit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (toasterSlotOccupied == null && (other.transform.gameObject.name == "ToasterSlot1" || other.transform.gameObject.name == "ToasterSlot2"))
        {
            if (other.transform.parent.Find("Fire(Clone)") == null) {
                handscript.DropHeld();
                toasterSlotOccupied = other.transform.gameObject;
                other.transform.localPosition = new Vector3(other.transform.localPosition.x, 0, 0);
                this.transform.SetParent(toasterSlotOccupied.transform);
                this.transform.localPosition = new Vector3(0.3f, 0f, 0);
                this.transform.localRotation = Quaternion.identity;
                this.GetComponent<MeshCollider>().isTrigger = true;
                this.GetComponent<Rigidbody>().isKinematic = true;
                this.gameObject.layer = 0; //not grabbable
                bagelscript.numCookingBagels += 1;
                print("Entered a toast! Total: " + bagelscript.numCookingBagels);
            }
            else
            {
                //oh no the toaster is on fire
            }
        }
    }

    public void ForceExit()
    {
        if (toasterSlotOccupied != null)
        {
            toasterSlotOccupied.transform.DetachChildren();
            toasterSlotOccupied.transform.localPosition = new Vector3(toasterSlotOccupied.transform.localPosition.x, 0.4f, 0);
            toasterSlotOccupied = null;
            bagelscript.numCookingBagels -= 1;
        }
        this.gameObject.layer = 11; //grabbable
        this.transform.position = this.transform.position + new Vector3(1, 0, 0); //"spring" to prevent glitchy interaction
        this.GetComponent<MeshCollider>().isTrigger = false;
        if (this.gameObject.layer != 8)
        {
            this.GetComponent<Rigidbody>().isKinematic = false;
        }
        print("Exited a toast");
    }
}
