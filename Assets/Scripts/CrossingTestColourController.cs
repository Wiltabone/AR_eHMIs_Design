using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingTestColourController : MonoBehaviour
{

    bool green;
    Renderer crossingRenderer;


    // Use this for initialization
    void Start()
    {
        green = false;
        crossingRenderer = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
        {

            print("TEST");

            if (green)
            {
                crossingRenderer.material.SetColor("_Color", new Color(203, 0, 0));
                //GetComponent<MeshRenderer>().material.color = new Color(203, 0, 0);
                green = false;
            }

            else if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)

                if (!green)
                {
                    //colour green
                    crossingRenderer.material.SetColor("_Color", new Color(42, 207, 7));
                    //GetComponent<MeshRenderer>().material.color = new Color(42, 207, 7);
                    green = true;
                }


        }    }
}
