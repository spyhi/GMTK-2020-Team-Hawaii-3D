using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackoutController : MonoBehaviour
{
    public GameObject blackoutCanvas;

    public GameObject bigFriendlyBox;
    public GameObject naughtyBox1;
    public GameObject naughtyBox2;
    public GameObject naughtyBox3;
    public GameObject toaster;
    public GameObject bagel;
    public GameObject bagelHalf1;
    public GameObject bagelHalf2;
    public GameObject knife;
    public GameObject player;

    public Bagels bagelscript;

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
        object1.transform.position = object2.transform.position;
        object2.transform.position = tempPos;
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
        //todo: fire
    }



    void Blackout()
    {
        blackoutCounter++;
        blackoutCanvas.SetActive(true);
        Time.timeScale = 20; //gives objects quick time to "settle". (quick fix)
        StartCoroutine(wakeMeUp());

        switch (blackoutCounter)
        {
            case 1:

                SwapObjects(bigFriendlyBox, naughtyBox1);
                SwapObjects(toaster, knife);
                break;
            case 2:
                SwapObjects(bigFriendlyBox, naughtyBox2);
                MergeBagel();
                SwapObjects(toaster, bagel);
                break;
            case 3:
                SwapObjects(bigFriendlyBox, naughtyBox3);
                MergeBagel();
                SwapObjects(bagel, naughtyBox2);
                break;
            case 4:
                SwapObjects(bigFriendlyBox, naughtyBox1);
                SwapObjects(toaster, naughtyBox2);
                SplitBagel();
                SwapObjects(bagelHalf1, naughtyBox3);
                PlaceObject(bagelHalf2, new Vector3(0, 10));
                SwapObjects(knife, player);
                break;
            default:
                MergeBagel();
                SwapObjects(bagel, player);
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
