using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoomingAnimationController : MonoBehaviour
{

    private Animator animator;
    public bool stateFlag = false; //T=yielding, F=non-yielding

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
        {
            if(stateFlag)
            {
                //play safe looming animation
                animator = GetComponent<Animator>();
                animator.Play("Concept_3_Safe_Anim");
            }
            else
            {
                //play unsafe looming animation
                animator = GetComponent<Animator>();
                animator.Play("Concept_3_Unsafe_Anim");
            }
           
        }
    }
}
