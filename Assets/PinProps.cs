using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PinProps : Singleton<PinProps>
{

    public string nameObj;
    public string cords;
    public int pNo;

    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI nameT;
    [SerializeField] TextMeshProUGUI cordsT;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("getName", 1);
        TextAsset asset = Resources.Load("Data") as TextAsset;
        if(asset != null)
        {

            
            //foreach (PinProps pin in pins)
            {
                
            }
        }

        panel.SetActive(false);

    }

    public void getName()
    {
        nameObj = gameObject.GetComponentInParent<Transform>().name;
        cordsT.text = "Coordinates " + cords;
        nameT.text = nameObj;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Marker")) 
                {

                    panel.SetActive(true);
                    Invoke("ClosePanel", 2);
                }
                else
                {
                    Debug.Log("This isn't a Marker");
                }
            }

        }
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }
}


