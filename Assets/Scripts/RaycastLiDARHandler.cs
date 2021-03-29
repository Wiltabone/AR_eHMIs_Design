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

    public static float planeScaleFactor = 0.0f;


    void Update()
    {

        //ray cast from centre of screen and get distance from device to hitpoint
        if (arRaycastManager.Raycast(rayPos, hits, TrackableType.Planes))
        {
            distance = hits[0].distance;
            print("DISTANCE: "+distance);

            planeScaleFactor = scaleFactorCalc(distance);
            print("ScaleFactor: " + planeScaleFactor);

        }

        //re-size plane object

    }

    //returns the scale factor to use for the plane
    private float scaleFactorCalc(float distance)
    {
        //normalise LiDAR distance to map between 0-5m to scale factor 0-1
        float sF = normalise(distance, 0f, 5f);
          
        return sF;
    }


    /*
        //remaps a float value from one scale to another (in this case, LiDAR distance 0 - 5m to scale factor 0 - 1)
        private float reMap(float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
    */


  
    private float normalise(float value, float min, float max)
    {

        float normalisedValue = (value - min) / (max - min);

        return normalisedValue;
    }
}
