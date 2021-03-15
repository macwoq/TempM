using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyOver : MonoBehaviour
{

    public float speed = 10;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var h = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        var v = Input.GetAxis("Vertical") * speed * Time.deltaTime;


        transform.Translate(h, v, 0);
    }
}
