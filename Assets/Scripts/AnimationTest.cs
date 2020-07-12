using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    Animator anim;
    public GameObject cabDoor;
    Collider cabDoorCollider;

    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        cabDoorCollider = cabDoor.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            anim.SetTrigger("Active");
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Open_Door"))
        {
            anim.ResetTrigger("Active");

            cabDoorCollider.enabled = false;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Close_Door"))
        {
            anim.ResetTrigger("Active");
            cabDoorCollider.enabled = true;

        }
    }
}
