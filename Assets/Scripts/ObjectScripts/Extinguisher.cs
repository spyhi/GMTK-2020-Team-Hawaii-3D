using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    public Collider blastZone;
    Rigidbody myrigidbody;
    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.layer == 8) //if being held snap position and rotation
        {
            gameObject.transform.localPosition = new Vector3(0.87f, -0.55f, -0.65f);
            gameObject.transform.localRotation = Quaternion.Euler(-20, -175, -13);
            blastZone.enabled = true;
        } else
        {
            blastZone.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (blastZone.enabled)
        {
            if (other.tag == "Fire")
            {
                GameObject.Destroy(other.gameObject);
                print("Fire found!");
            }
            else
            {
                print("Hit not fire...");
            }
        }
    }

}
