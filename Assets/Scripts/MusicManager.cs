using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AK.Wwise.Event currentMusic;
    public List<AK.Wwise.Switch> switchSets = new List<AK.Wwise.Switch>();
    //public AK.Wwise.Switch switchSet;

    public GameObject CompletionProgress;
    private BlackoutController currentProgress;
    static System.Random rnd = new System.Random();
    int switchNum;

    float timer = 99.999f;

    // Start is called before the first frame update
    void Start()
    {
        switchSets[0].SetValue(gameObject);
        currentMusic.Post(gameObject);
        currentProgress = CompletionProgress.GetComponent<BlackoutController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentProgress.completion < .75)
        {
            switchSets[1].SetValue(gameObject);
        }

        else if(currentProgress.completion < .81)
        {
            switchSets[2].SetValue(gameObject);
        }
        else if(currentProgress.completion < .90)
        {
            switchSets[3].SetValue(gameObject);
        }


        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    //Debug.Log(switchSets.Count);
        //    switchNum = rnd.Next(0, 3);
            
        //    //Debug.Log("Random Number: " + switchNum);
        //    //Debug.Log(switchSets[switchNum]);
        //    switchSets[switchNum].SetValue(gameObject);
        //}
        
    }
}
