using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;

public class spawnOnFly : MonoBehaviour
{
    [SerializeField]
    private GameObject placeableObjectPrefabs;

    private GameObject currentPlaceableObject;

    private KeyCode ObjectHotKey = KeyCode.B;

    private float mouseWheelRotation;
    private int currentPrefabIndex = -1;

    [SerializeField] public AbstractMap _map;
    private void Update()
    {
        HandleNewObjectHotkey();

        if (currentPlaceableObject != null)
        {
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
        }
    }

    private void HandleNewObjectHotkey()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {

            if (currentPlaceableObject != null)
            {
                Destroy(currentPlaceableObject);
            }
            else
            {
                currentPlaceableObject = Instantiate(placeableObjectPrefabs);
                Debug.Log("OBJECT INSTANTIATED");

            }
        }
    }


    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            currentPlaceableObject.transform.position = hitInfo.point;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    private void RotateFromMouseWheel()
    {
        //Debug.Log(Input.mouseScrollDelta);
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 5f);
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {

            currentPlaceableObject = null;

        }

    }


}