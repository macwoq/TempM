using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField]GameObject holder;
    [SerializeField]
    Camera cam;
    Vector3 previousPos = Vector3.zero;
    Vector3 deltaPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        //holder = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //CamControl();
    }

    public void UpdateTransformCamera(bool isLocked)
    {
        if (isLocked)
        {
            CamControl();
        }
    }

    

    void CamControl()
    {
        deltaPos = holder.transform.position - previousPos;
        deltaPos.y = 0;
        cam.transform.position = Vector3.Lerp(cam.transform.position, cam.transform.position + deltaPos, Time.time);
        previousPos = transform.position;
        print(deltaPos);
    }
}
