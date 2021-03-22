using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class RaycastLiDARHandler : MonoBehaviour
{
    public ARRaycastManager arRaycastManager;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private Vector2 rayPos = new Vector2(Screen.width/2, Screen.height/2);

    private float distance;

    
    void Start()
    {
        
    }



    void Update()
    {

        //if (Input.touchCount == 0)
        //return;

        if (arRaycastManager.Raycast(rayPos, hits, TrackableType.Planes))
        //if (arRaycastManager.Raycast(rayPos, hits))
        {
            distance = hits[0].distance;
            print("DISTANCE: "+distance);
        }
    }
}
