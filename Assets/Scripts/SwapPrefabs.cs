using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPrefabs : MonoBehaviour
{

    public GameObject newObj;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
        {
            Instantiate(newObj, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
