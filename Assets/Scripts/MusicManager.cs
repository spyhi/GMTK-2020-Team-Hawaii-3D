using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AK.Wwise.Event currentMusic;
    public AK.Wwise.Switch switchSet;

    // Start is called before the first frame update
    void Start()
    {
        switchSet.SetValue(gameObject);
        currentMusic.Post(gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
