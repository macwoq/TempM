using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCenter : MonoBehaviour
{
    public Vector3 offset;

    GameObject player;


    public Camera MainCamera;
    public GameObject TargetPosition;
    public int speed = 2;
    bool camMove = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (camMove)
        {
            MainCamera.transform.position = Vector3.Lerp(transform.position, TargetPosition.transform.position, speed * Time.deltaTime);
            MainCamera.transform.rotation = Quaternion.Lerp(transform.rotation, TargetPosition.transform.rotation, speed * Time.deltaTime);
        }
    }

    public void CenterPos()
    {
        transform.position = player.transform.position + offset;
    }



    public void CameraResetButton()
    {
        MainCamera.transform.position = new Vector3(player.transform.position.x,player.transform.position.y,player.transform.position.z) + offset;
        //TargetPosition.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
