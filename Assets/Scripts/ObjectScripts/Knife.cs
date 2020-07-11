using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public Bagels bagelscript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.layer == 8) //if being held snap position and rotation
        {
            gameObject.transform.localPosition = new Vector3(0.44f, -0.33f, -0.65f);
            gameObject.transform.localRotation = Quaternion.Euler(-30, -100, -60);
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
