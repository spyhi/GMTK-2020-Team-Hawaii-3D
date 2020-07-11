using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bagels : MonoBehaviour
{
    public Transform fullBagel;
    public Transform halfBagel1;
    public Transform halfBagel2;
    public BagelHalf halfBagelScript1;
    public BagelHalf halfBagelScript2;
    public int numCookingBagels = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SplitBagel()
    {
        if (fullBagel.gameObject.activeInHierarchy)
        {
            halfBagel1.gameObject.SetActive(true);
            halfBagel2.gameObject.SetActive(true);
            fullBagel.gameObject.SetActive(false);
            halfBagel1.position = fullBagel.position;
            halfBagel2.position = fullBagel.position;
            halfBagel1.rotation = fullBagel.rotation;
            halfBagel2.rotation = fullBagel.rotation;
        }

    }

    public void SealBagel()
    {
        print(fullBagel.gameObject.activeInHierarchy);
        if (!fullBagel.gameObject.activeInHierarchy)
        {
            halfBagelScript1.ForceExit();
            halfBagelScript2.ForceExit();
            fullBagel.position = halfBagel1.position;
            fullBagel.rotation = halfBagel1.rotation;
            halfBagel1.gameObject.SetActive(false);
            halfBagel2.gameObject.SetActive(false);
            fullBagel.gameObject.SetActive(true);
        }
    }
}
