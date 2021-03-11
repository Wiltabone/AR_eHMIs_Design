using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMat : MonoBehaviour
{
    public Material new_mat;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.material = new_mat;
        }
    }
}
