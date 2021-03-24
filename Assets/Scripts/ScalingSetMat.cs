using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingSetMat : MonoBehaviour
{
    public Material new_mat;

    private bool swapped = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.material = new_mat;
        }
    }
}
