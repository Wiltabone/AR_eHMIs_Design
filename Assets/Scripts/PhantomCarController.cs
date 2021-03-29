using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomCarController : MonoBehaviour
{
    private Renderer renderer;
    public Material new_mat;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
        {
            //shift position of phantom car to be before crossing
            this.GetComponent<Transform>().transform.Translate(1f, 0f, 0f, Space.World);

            renderer = GetComponent<Renderer>();
            //swap materials
            var mats = renderer.materials;

            for(int i=0; i< mats.Length; i++)
            {
                mats[i] = new_mat;
            }
            renderer.materials = mats;
        }
    }
}
