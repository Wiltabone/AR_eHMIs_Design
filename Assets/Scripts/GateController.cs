using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//08/03/2021
//This class changes the colour of the gate and then plays its open animation.
public class GateController : MonoBehaviour
{
    //private Renderer renderer;
    private Animator animator;
    public Material open_mat;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
        {
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            
            //switch colour for each renderer of child objects
            foreach (var r in renderers)
            {
                r.material = open_mat;
            }

            //play open animation
            print("ANIM PLAY");
            animator = GetComponent<Animator>();
            animator.Play("openGate");
        
        }
    }
}
