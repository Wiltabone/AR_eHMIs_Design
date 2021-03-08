using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapMat : MonoBehaviour
{
    private Renderer renderer;
    public Material first_mat;
    public Material second_mat;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
        {
            renderer = GetComponent<Renderer>();
            //swap materials
            var mats = renderer.materials;
            mats[1] = first_mat;
            mats[2] = second_mat;
            renderer.materials = mats;
        }
    }
}
