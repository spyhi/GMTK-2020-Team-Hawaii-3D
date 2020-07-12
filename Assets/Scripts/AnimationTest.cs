using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    Animator anim;
    public GameObject cabDoor;
    Collider cabDoorCollider;
    public bool cabDoorOn;
    private bool canBeClicked = false;
    public Collider myBounds;
    public Collider playerHandBounds;
    public Light controlledLight;

    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        if (cabDoorOn) { cabDoorCollider = cabDoor.GetComponent<BoxCollider>(); }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && myBounds.bounds.Intersects(playerHandBounds.bounds))
        {
            anim.SetTrigger("Active");
            if (!cabDoorOn && controlledLight.enabled == true) { controlledLight.enabled = false; }
            else if (!cabDoorOn && controlledLight.enabled == false) { controlledLight.enabled = true; }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Open_Door"))
        {
            anim.ResetTrigger("Active");

            
            if (cabDoorOn) { cabDoorCollider.enabled = false; }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Close_Door"))
        {
            anim.ResetTrigger("Active");
            
            if (cabDoorOn) { cabDoorCollider.enabled = true; }

        }
    }
}
