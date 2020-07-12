using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    public Collider blastZone;
    public GameObject smoke;
    private ParticleSystem system;
    
    Rigidbody myrigidbody;
    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = this.GetComponent<Rigidbody>();
        system = smoke.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.layer == 8) //if being held snap position and rotation
        {
            gameObject.transform.localPosition = new Vector3(0.87f, -0.55f, -0.65f);
            gameObject.transform.localRotation = Quaternion.Euler(-20, -175, -13);
            blastZone.enabled = true;
            smoke.SetActive(true);
            system.Play();
        } else
        {
            blastZone.enabled = false;
            //for whatever reason .Stop() seems to kill it permenantly
            system.Pause();
            // system.Stop(false, ParticleSystemStopBehavior.StopEmitting);
            smoke.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (blastZone.enabled)
        {
            if (other.tag == "Fire")
            {
                print("Fire found!");
                GameObject.Destroy(other.gameObject);
            }
            else
            {
                print("Hit not fire...");
            }
        }
    }
}
