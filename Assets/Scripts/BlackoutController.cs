using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject miscStarbucks;
    public GameObject miscStarbucks1;
    public GameObject miscStarbucks2;
    public GameObject miscFlowerpot;
    public GameObject miscChair;
    public GameObject miscChair1;
    public GameObject miscMug;

    public Light mainLight;

    public GameObject FireInstantiable;
    public Bagels bagelscript;
    public hand_controller handscript;
    public FP_Controller movescript;

    public Text objectivetext;
    public Text descriptortext;

    private List<GameObject> clones = new List<GameObject>();

    int blackoutCounter;
    float timer;

    public float completion;
    // Start is called before the first frame update
    void Start()
    {
        blackoutCanvas.SetActive(false);
        blackoutCounter = 0;
        timer = 99.999f;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateCompletion();
        reduceTime();        
        //print(completion);
        if ((Input.GetKeyDown(KeyCode.V) || completion >= 1 || timer == 0) && blackoutCounter < 5)
        {
            Blackout();
        }


    }

    void reduceTime()
    {
        if (completion < .50)
        {
            timer = Mathf.Max(timer - Time.deltaTime * 1, 0);
            objectivetext.color = new Color(1, 1, 1);
        }
        else if (completion < .75)
        {
            timer = Mathf.Max(timer - Time.deltaTime * 2, 0);
            objectivetext.color = new Color(1, 0.75f, 0);
        }
        else if (completion < .81)
        {
            timer = Mathf.Max(timer - Time.deltaTime * 3, 0);
            objectivetext.color = new Color(1, 0.35f, 0);
        }
        else if (completion < .90)
        {
            timer = Mathf.Max(timer - Time.deltaTime * 6, 0);
            objectivetext.color = new Color(0.9f, 0, 0);
        }
        else if (completion < .96)
        {
            timer = Mathf.Max(timer - Time.deltaTime * 20, 0);
            objectivetext.color = new Color(0.5f, 0, 0);
        } else
        { 
            timer = Mathf.Max(timer - Time.deltaTime * 1000, 0);
            objectivetext.color = new Color(0.5f, 0, 0);
        }
        objectivetext.text = "00:" + Mathf.Round(timer * 1000) / 1000;
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
            newFire.transform.localPosition = new Vector3(0, 0, 0);
            newFire.SetActive(true);
        }
    }

    void Extinguish(GameObject object1)
    {
        if (object1.transform.Find("Fire(Clone)") != null)
        {
            GameObject.Destroy(object1.transform.Find("Fire(Clone)").gameObject);
        }
    }
    void Pickup(GameObject object1)
    {
        handscript.Pickup(object1);
    }
    void CloneObject(GameObject object1, Vector3 position)
    {
        GameObject newObject = Instantiate<GameObject>(object1, position, Quaternion.identity);
        clones.Add(newObject);
    }
    void destroyAllClones()
    {
        while (clones.Count > 0)
        {
            GameObject.Destroy(clones[0]);
            clones.Remove(clones[0]);
        }
    }



    void Blackout()
    {
        blackoutCounter++;
        blackoutCanvas.SetActive(true);
        handscript.DropHeld();
        bagelHalf1.GetComponent<BagelHalf>().ForceExit();
        bagelHalf2.GetComponent<BagelHalf>().ForceExit();
        completion = 0;
        movescript.movementAllowed = false;
        Time.timeScale = 20; //gives objects quick time to "settle". (quick fix)
        StartCoroutine(wakeMeUp());
        timer = 100;

        switch (blackoutCounter)
        {
            case 1:
                SwapObjects(toaster, flowerpot);
                Pickup(flowerpot);
                SwapObjects(bagelHalf1, knife);
                PlaceObject(bagelHalf2, new Vector3(3.7f, 2.5f, 3));
                SetOnFire(toaster);
                break;
            case 2:
                MergeBagel();
                PlaceObject(toaster, new Vector3(3.7f, 2.5f, 1));
                PlaceObject(knife, new Vector3(-18, 0, -2));
                SetOnFire(knife);
                break;
            case 3:
                SplitBagel();
                PlaceObject(bagelHalf1, new Vector3(1.9f, 1.5f, 2.7f));
                PlaceObject(flowerpot, new Vector3(-4, 0.5f, -9));
                mainLight.enabled = false;
                PlaceObject(bagelHalf2, new Vector3(5.13f, 3.63f, 6.96f));
                bagelHalf2.transform.rotation = Quaternion.Euler(0, 0, -90);
                bagelHalf2.GetComponent<Rigidbody>().isKinematic = true;
                //done: turn off lights, give the man a bagel face
                break;
            case 4:
                PlaceObject(bagelHalf1, new Vector3(-2.59f, 2.37f, 10.03f));
                for (int i = 0; i < 20; i++)
                {
                    CloneObject(miscChair, new Vector3(-4, 5, -13));
                    CloneObject(miscChair1, new Vector3(-4, 5, -13));
                }
                MergeBagel();
                PlaceObject(bagel, new Vector3(-11.46f, 3.3f, 9.6f));
                PlaceObject(knife, new Vector3(-20.9f, 1.3f, -3.2f));
                PlaceObject(toaster, new Vector3(-2.4f, 2.7f, 9.6f));
                Extinguish(knife);
                Extinguish(toaster);
                extinguisher.SetActive(false);
                flowerpot.SetActive(false);
                miscStarbucks.SetActive(false);
                miscStarbucks1.SetActive(false);
                miscStarbucks2.SetActive(false);
                miscFlowerpot.SetActive(false);
                miscChair.SetActive(false);
                miscChair1.SetActive(false);
                miscMug.SetActive(false);
                break;
            default:
                //no time limit for final phase, (maybe remove timer from UI?)
                destroyAllClones();
                extinguisher.SetActive(true);
                flowerpot.SetActive(true);
                miscStarbucks.SetActive(true);
                miscStarbucks1.SetActive(true);
                miscStarbucks2.SetActive(true);
                miscFlowerpot.SetActive(true);
                miscChair.SetActive(true);
                miscChair1.SetActive(true);
                miscMug.SetActive(true);
                MergeBagel();
                PlaceObject(bagel, new Vector3(-1, 3f, 1f));
                PlaceObject(knife, new Vector3(-1f, 3f, 4f));
                Extinguish(bagel);
                Extinguish(toaster);
                Extinguish(knife);
                SetOnFire(extinguisher);
                SetOnFire(flowerpot);
                SetOnFire(miscStarbucks);
                SetOnFire(miscStarbucks1);
                SetOnFire(miscStarbucks2);
                SetOnFire(miscFlowerpot);
                SetOnFire(miscChair);
                SetOnFire(miscChair1);
                SetOnFire(miscMug);
                mainLight.intensity = mainLight.intensity * 0.4f;
                //todone: set more things on fire
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
            case 3:
                if (mainLight.enabled == true)
                {
                    //lights on
                    completion = Mathf.Max(completion, 0.5f);
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
            case 4:
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
                completion = 0;
                descriptortext.text = "";
                objectivetext.enabled = false;
                if (bagelscript.numCookingBagels == 2)
                {
                    //bagels made it into toaster, 1.0
                    completion = 1;
                    descriptortext.text = "You're winner";
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

        timer = 99.999f;
        movescript.movementAllowed = true;
        for (int i = 0; i < 30; i++)
        {
            blackoutCanvas.GetComponent<Image>().color = new Color(0, 0, 0, 1 - (i * 0.033f));
            yield return null;
        }
        blackoutCanvas.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        blackoutCanvas.SetActive(false);
    }
}
