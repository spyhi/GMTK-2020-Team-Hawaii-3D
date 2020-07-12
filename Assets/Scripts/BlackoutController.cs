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

    public float completion;
    // Start is called before the first frame update
    void Start()
    {
        blackoutCanvas.SetActive(false);
        blackoutCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateCompletion();
        print(completion);
        if(Input.GetKeyDown(KeyCode.V) || completion >= 1)
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
    void Pickup(GameObject object1)
    {
        handscript.Pickup(object1);
    }



    void Blackout()
    {
        blackoutCounter++;
        blackoutCanvas.SetActive(true);
        handscript.DropHeld();
        bagelHalf1.GetComponent<BagelHalf>().ForceExit();
        bagelHalf2.GetComponent<BagelHalf>().ForceExit();
        completion = 0;
        Time.timeScale = 20; //gives objects quick time to "settle". (quick fix)
        StartCoroutine(wakeMeUp());

        switch (blackoutCounter)
        {
            case 1:
                Pickup(flowerpot);
                PlaceObject(toaster, new Vector3(-3, 2, 3));
                SwapObjects(bagelHalf1, knife);
                PlaceObject(bagelHalf2, new Vector3(-3.7f, 0, -9));
                SetOnFire(toaster);
                break;
            case 2:
                MergeBagel();
                SwapObjects(toaster, bagel);
                PlaceObject(knife, new Vector3(0, 0, 4.4f));
                SetOnFire(knife);
                break;
            case 3:
                MergeBagel();
                //todo: turn off lights, give the man a bagel face
                break;
            case 4:
                //todo: reinvent this whole phase
                SwapObjects(bigFriendlyBox, testBox1);
                SwapObjects(toaster, testBox2);
                SplitBagel();
                SwapObjects(bagelHalf1, testBox3);
                PlaceObject(bagelHalf2, new Vector3(0, 10));
                SwapObjects(knife, player);
                break;
                //todo: maybe add additional phase here?
            default:
                //no time limit for final phase, (maybe remove timer from UI?)
                SwapObjects(bagel, player);
                MergeBagel();
                Extinguish(bagel);
                Extinguish(toaster);
                Extinguish(knife);
                SetOnFire(extinguisher);
                SetOnFire(flowerpot);
                //todo: set more things on fire
                break;
        }
    }

    void CalculateCompletion()
    {
        switch(blackoutCounter)
        {
            case 0:
                if (handscript.currentHeldObject.name == "Knife")
                {
                    //holding knife, 0.25
                    completion = Mathf.Max(completion, 0.25f);
                }
                if (!bagelscript.fullBagel.gameObject.activeInHierarchy)
                {
                    //bagel split, 0.5
                    completion = Mathf.Max(completion, 0.5f);
                } if ((handscript.currentHeldObject.name == "BagelHalf1" || handscript.currentHeldObject.name == "BagelHalf2"))
                {
                    //holding 1 bagel, 0.6-0.8
                    completion = Mathf.Max(completion, 0.6f + calculateDistanceFactor(handscript.currentHeldObject, toaster, 0.2f));
                }
                if (bagelscript.numCookingBagels == 1)
                {
                    //cooking 1 bagel, 0.85-1.0
                    completion = Mathf.Max(completion, 0.85f + Mathf.Min(calculateDistanceFactor(bagelHalf1, toaster, 0.15f), calculateDistanceFactor(bagelHalf2, toaster, 0.15f)));
                }
                if (bagelscript.numCookingBagels == 1 &&(handscript.currentHeldObject.name == "BagelHalf1" || handscript.currentHeldObject.name == "BagelHalf2"))
                {
                    //cooking 1 & holding 1 bagel, 0.9-1.0
                    completion = Mathf.Max(completion, 0.9f + calculateDistanceFactor(handscript.currentHeldObject, toaster, 0.1f));
                    //100% completion *HARD CUTOFF* when bagel 1 unit from toaster
                }
                if (bagelscript.numCookingBagels == 2)
                {
                    //bagels made it into toaster, 1.0
                    completion = 1;
                    print("Player finished! Somehow?");
                }
                break;
            case 1:
                if (handscript.currentHeldObject.name == "fire_extinguisher nozzleless")
                {
                    //holding fire extinguisher, 0.25-0.45
                    completion = Mathf.Max(completion, 0.25f + calculateDistanceFactor(handscript.currentHeldObject, toaster, 0.2f));
                }
                if (toaster.transform.Find("Fire(Clone)") == null)
                {
                    //toaster fire extinguished, 0.5-0.7
                    completion = Mathf.Max(completion, 0.5f + calculateDistanceFactor(bagelHalf1, toaster, 0.1f) + calculateDistanceFactor(bagelHalf2, toaster, 0.1f));
                }
                if (toaster.transform.Find("Fire(Clone)") == null && (handscript.currentHeldObject.name == "BagelHalf1" || handscript.currentHeldObject.name == "BagelHalf2"))
                {
                    //fire extinguished + holding half bagel, 0.6-0.8
                    completion = Mathf.Max(completion, 0.6f + calculateDistanceFactor(handscript.currentHeldObject, toaster, 0.2f));
                }
                if (bagelscript.numCookingBagels == 1)
                {
                    //cooking 1 bagel, 0.85-1.0
                    completion = Mathf.Max(completion, 0.85f + Mathf.Min(calculateDistanceFactor(bagelHalf1, toaster, 0.15f), calculateDistanceFactor(bagelHalf2, toaster, 0.15f)));
                }
                if (bagelscript.numCookingBagels == 1 && (handscript.currentHeldObject.name == "BagelHalf1" || handscript.currentHeldObject.name == "BagelHalf2"))
                {
                    //cooking 1 bagel & holding 1 bagel, 0.9-1.0
                    completion = Mathf.Max(completion, 0.9f + calculateDistanceFactor(handscript.currentHeldObject, toaster, 0.1f));
                    //100% completion *HARD CUTOFF* when bagel 1 unit from toaster
                }
                if (bagelscript.numCookingBagels == 2)
                {
                    //bagels made it into toaster, 1.0
                    completion = 1;
                    print("Player finished! Somehow?");
                }
                break;
            case 2:
                if (knife.transform.Find("Fire(Clone)") == null)
                {
                    //knife fire extinguished, 0.25
                    completion = Mathf.Max(completion, 0.25f);
                }
                if (handscript.currentHeldObject.name == "Knife")
                {
                    //holding knife, 0.4
                    completion = Mathf.Max(completion, 0.4f);
                }
                if (!bagelscript.fullBagel.gameObject.activeInHierarchy)
                {
                    //bagel split, 0.5-0.7
                    completion = Mathf.Max(completion, 0.5f + calculateDistanceFactor(bagelHalf1, toaster, 0.1f) + calculateDistanceFactor(bagelHalf2, toaster, 0.1f));
                }
                if ((handscript.currentHeldObject.name == "BagelHalf1" || handscript.currentHeldObject.name == "BagelHalf2"))
                {
                    //bagel held, 0.6-0.8
                    completion = Mathf.Max(completion, 0.6f + calculateDistanceFactor(handscript.currentHeldObject, toaster, 0.2f));
                }
                if (bagelscript.numCookingBagels == 1)
                {
                    //cooking 1 bagel, 0.85-1.0
                    completion = Mathf.Max(completion, 0.85f + Mathf.Min(calculateDistanceFactor(bagelHalf1, toaster, 0.15f), calculateDistanceFactor(bagelHalf2, toaster, 0.15f)));
                }
                if (bagelscript.numCookingBagels == 1 && (handscript.currentHeldObject.name == "BagelHalf1" || handscript.currentHeldObject.name == "BagelHalf2"))
                {
                    //cooking 1 bagel and holding 1 bagel, 0.9-1.0
                    completion = Mathf.Max(completion, 0.9f + calculateDistanceFactor(handscript.currentHeldObject, toaster, 0.1f));
                    //100% completion *HARD CUTOFF* when bagel 1 unit from toaster
                }
                if (bagelscript.numCookingBagels == 2)
                {
                    //bagels made it into toaster, 1.0
                    completion = 1;
                    print("Player finished! Somehow?");
                }
                break;
            default:
                //Placeholder logic for unfinished levels
                if (bagelscript.numCookingBagels == 1)
                {
                    //cooking 1 bagel, 0.75-1.0
                    completion = 0.75f + Mathf.Min(calculateDistanceFactor(bagelHalf1, toaster, 0.25f), calculateDistanceFactor(bagelHalf2, toaster, 0.25f));
                }
                if (bagelscript.numCookingBagels == 2)
                {
                    //bagels made it into toaster, 1.0
                    completion = 1;
                    print("Player finished! Somehow?");
                }
                break;
        }
    }

    float calculateDistanceFactor(GameObject target, GameObject goal, float max)
    {
        //scale of 6 - 1 units
        float distance = Vector3.Distance(target.transform.position, goal.transform.position);
        float factor = (1 - ((distance - 1f) / 5)) * max;
        return Mathf.Max(0, Mathf.Min(max, factor));
    }

    IEnumerator wakeMeUp()
    {
        yield return new WaitForSeconds(10f); //actually 0.5 seconds after timescale
        Time.timeScale = 1;
        blackoutCanvas.SetActive(false);
    }
}
