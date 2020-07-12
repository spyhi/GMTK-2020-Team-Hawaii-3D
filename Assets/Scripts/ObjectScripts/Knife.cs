using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public Bagels bagelscript;
    public float xVector;
    public float yVector;
    public float zVector;

    public float xQuat;
    public float yQuat;
    public float zQuat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.layer == 8) //if being held snap position and rotation
        {
            gameObject.transform.localPosition = new Vector3(xVector, yVector, zVector);
            gameObject.transform.localRotation = Quaternion.Euler(xQuat, yQuat, zQuat);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bagel") //hacky code, detect bagel and enable "bagel halves"
        {
            bagelscript.SplitBagel();
        }
    }
}
