using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackoutController : MonoBehaviour
{
    public GameObject blackoutCanvas;

    public GameObject bigFriendlyBox;
    public GameObject testBox1;
    public GameObject testBox2;
    public GameObject testBox3;
    public GameObject toaster;
    public GameObject bagel;
    public GameObject bagelHalf1;
    public GameObject bagelHalf2;
    public GameObject knife;
    public GameObject player;
    public GameObject extinguisher;
    public GameObject flowerpot;

    public GameObject FireInstantiable;
    public Bagels bagelscript;
    public hand_controller handscript;

    int blackoutCounter;
    // Start is called before the first frame update
    void Start()
    {
        blackoutCanvas.SetActive(false);
        blackoutCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            Blackout();
        }
    }

    void SwapObjects(GameObject object1, GameObject object2)
    {
        Vector3 tempPos = object1.transform.position;
        Quaternion tempRot = object1.transform.rotation;
        object1.transform.position = object2.transform.position + new Vector3(0, 1.5f, 0);
        object2.transform.position = tempPos + new Vector3(0, 1.5f, 0);
        if (object1 != player)
        {
            object1.transform.rotation = object2.transform.rotation;
        }
        if (object2 != player)
        {
            object2.transform.rotation = tempRot;
        }
        
    }

    void PlaceObject(GameObject object1, Vector3 position)
    {
        object1.transform.position = position;
    }

    void MergeBagel()
    {
        bagelscript.SealBagel();
    }

    void SplitBagel()
    {
        bagelscript.SplitBagel();
    }

    void SetOnFire(GameObject object1)
    {
        if (object1.transform.Find("Fire(Clone)") == null)
        {
            GameObject newFire = Instantiate<GameObject>(FireInstantiable, object1.transform);
            newFire.SetActive(true);
        }
    }

    void Extinguish(GameObject object1)
    {
        GameObject.Destroy(object1.transform.Find("Fire(Clone)"));
    }
    void pickup(GameObject object1)
    {
        handscript.Pickup(object1);
    }



    void Blackout()
    {
        blackoutCounter++;
        blackoutCanvas.SetActive(true);
        handscript.DropHeld();
        Time.timeScale = 20; //gives objects quick time to "settle". (quick fix)
        StartCoroutine(wakeMeUp());

        switch (blackoutCounter)
        {
            case 1:
                pickup(flowerpot);
                SwapObjects(bigFriendlyBox, testBox1);
                SwapObjects(toaster, knife);
                SetOnFire(toaster);
                break;
            case 2:
                SwapObjects(bigFriendlyBox, testBox2);
                MergeBagel();
                SwapObjects(toaster, bagel);
                break;
            case 3:
                SwapObjects(bigFriendlyBox, testBox3);
                MergeBagel();
                SwapObjects(bagel, testBox2);
                break;
            case 4:
                SwapObjects(bigFriendlyBox, testBox1);
                SwapObjects(toaster, testBox2);
                SplitBagel();
                SwapObjects(bagelHalf1, testBox3);
                PlaceObject(bagelHalf2, new Vector3(0, 10));
                SwapObjects(knife, player);
                break;
            default:
                SwapObjects(bagel, player);
                MergeBagel();
                Extinguish(bagel);
                Extinguish(toaster);
                Extinguish(knife);
                SetOnFire(extinguisher);
                SetOnFire(bigFriendlyBox);
                SetOnFire(testBox1);
                SetOnFire(testBox2);
                SetOnFire(testBox3);

                break;
        }
    }

    IEnumerator wakeMeUp()
    {
        yield return new WaitForSeconds(10f); //actually 0.5 seconds after timescale
        Time.timeScale = 1;
        blackoutCanvas.SetActive(false);
    }
}
