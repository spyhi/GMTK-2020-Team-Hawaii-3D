using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AK.Wwise.Event currentMusic;
    public List<AK.Wwise.Switch> switchSets = new List<AK.Wwise.Switch>();
    //public AK.Wwise.Switch switchSet;

    static System.Random rnd = new System.Random();
    int switchNum;

    // Start is called before the first frame update
    void Start()
    {
        switchSets[0].SetValue(gameObject);
        currentMusic.Post(gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            //Debug.Log(switchSets.Count);
            switchNum = rnd.Next(0, 3);
            
            //Debug.Log("Random Number: " + switchNum);
            //Debug.Log(switchSets[switchNum]);
            switchSets[switchNum].SetValue(gameObject);
        }
        
    }
}
