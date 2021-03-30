using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingByDistance : MonoBehaviour
{

    private bool swapped = false;
    private float scaleF = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scaleF = RaycastLiDARHandler.planeScaleFactor;

        //if unsafe set sF to be 1-sF
        if (swapped)
        {
            scaleF = 1f - scaleF;
        }

        //rescaling
        this.GetComponent<Transform>().transform.localScale = Vector3.one * scaleF;

        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
        {
            swapped = true;
        }
    }
}
