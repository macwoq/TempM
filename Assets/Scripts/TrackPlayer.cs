using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{

    Vector3 deltaPos = Vector3.zero, previousPos = Vector3.zero;
    [SerializeField] Camera cam;
    [SerializeField] GameObject player;
    public bool trackPlayer;
    public bool isTracking;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
        if (trackPlayer) { 
        CamControl();   
        }
    }

    public void SetTrackingActive(bool isTracking)
    {
        
        if (isTracking)
        {
            print(isTracking);
            trackPlayer = true;
        }
        else
        {
            print(isTracking);
            trackPlayer = false;
        }
    }

    public void CamControl()
    {
        deltaPos = transform.position - previousPos;
        deltaPos.y = 0;
        cam.transform.position = Vector3.Lerp(cam.transform.position, cam.transform.position + deltaPos, Time.time);
        previousPos = transform.position;
    }

 
}
