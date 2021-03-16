using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using Mapbox.Json.Converters;
using Mapbox.Json;
using Mapbox.Json.Linq;
using System.IO;
using TMPro;
using Mapbox.Utils.JsonConverters;

public class SpawnMarker : Singleton<SpawnMarker>
{

    public int pNumber;
    public string prefix = "Punkt ";

    public string[] descriptions;
    public string desc;
    [SerializeField] TextMeshProUGUI totalNo;
    public GameObject popUp;
    public TextMeshProUGUI popText;

    [SerializeField] AbstractMap map;
    [SerializeField] [Geocode] string[] locations;
    [SerializeField] [Geocode] List<string> loc = new List<string>();
    [SerializeField] Vector2d[] _locations;
    public List<string> positionsString = new List<string>();
    public List<Vector2d> positions = new List<Vector2d>();
    public float _spawnScale = 100;
    [SerializeField] GameObject marker;
    Camera cam;
    public List<GameObject> spawnedObj = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        popUp.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            SetMarker();

        }
        if (spawnedObj != null)
        {
            int count = spawnedObj.Count;
            for (int i = 0; i < count; i++)
            {
                var spawnedObject = spawnedObj[i];
                var location = positions[i];
                spawnedObject.transform.localPosition = map.GeoToWorldPosition(location, true);
                spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                spawnedObject.name = "Punkt " + i.ToString();
                //spawnedObject pNumber =  + i;

            }


        }
        totalNo.text = "Liczba punktów:  " + pNumber.ToString();
    }

    public void EditorCodedLocations()
    {
        _locations = new Vector2d[locations.Length];
        spawnedObj = new List<GameObject>();
        for (int i = 0; i < locations.Length; i++)
        {
            var locationString = locations[i];

            _locations[i] = Conversions.StringToLatLon(locationString);
            var instance = Instantiate(marker);
            instance.transform.localPosition = map.GeoToWorldPosition(_locations[i], true);
            instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            spawnedObj.Add(instance);
        }
    }

    public void SetMarker()
    {
        var playerPos = GameObject.Find("PlayerTarget");
        var mousePosScreen = Input.mousePosition;
        mousePosScreen.z = cam.transform.localPosition.y;
        var pos = cam.ScreenToWorldPoint(mousePosScreen);
        var camPos = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, Camera.main.nearClipPlane));
        var posPlayer = playerPos.transform.localPosition;

        var latlongDelta = map.WorldToGeoPosition(posPlayer);

        positions.Add(latlongDelta);

        positionsString.Add(latlongDelta.ToString());
        var c = Conversions.StringToLatLon(positionsString[0]);
        print(c.ToString());

        var instance = Instantiate(marker);
        instance.transform.localPosition = map.GeoToWorldPosition(positions[0], true);
        instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);

        spawnedObj.Add(instance);

        var giveCords = instance.GetComponent<PinProps>();

        giveCords.cords = latlongDelta.ToString();
        pNumber++;


    }


    public void ClearLists()
    {
        GameObject[] markers = GameObject.FindGameObjectsWithTag("Marker");
        for (int i = 0; i < markers.Length; i++)
        {
            Destroy(markers[i]);
        }
        pNumber = 0;
        spawnedObj.Clear();
        positions.Clear();
        positionsString.Clear();
        popUp.gameObject.SetActive(true);
        popText.text = "Usunięto!";
    }



    public void SaveIntoJson()
    {
        popUp.gameObject.SetActive(true);
        popText.text = "Zapisano!";
        
        string filePath = Application.persistentDataPath + "/pins.json";
        print("saving to: " + filePath);
        JObject jObject = new JObject();
        jObject.Add("componentName", GetType().ToString());



        JObject jDataObject = new JObject();
        jObject.Add("features", jDataObject);

        //jDataObject.Add("_name", );
        jDataObject.Add("totalPinsNumber", pNumber);

        JArray jFriendsArray = JArray.FromObject(positions);
        jDataObject.Add("coordinates", jFriendsArray);

        StreamWriter sw = new StreamWriter(filePath);
        sw.WriteLine(jObject.ToString());
        sw.Close();



    }

    public void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            string filePath = Application.persistentDataPath + "/pins.json";
            print("loading from: " + filePath);

            StreamReader sr = new StreamReader(filePath);
            string jsonString = sr.ReadToEnd();
            sr.Close();

            JObject jObj = JObject.Parse(jsonString);

            //locations = jObj["features"]["_name"].Value<string>();
            pNumber = jObj["features"]["totalPinsNumber"].Value<int>();
            //positionsString = jObj["data"]["_friends"].ToObject<string[]>();
            _locations = jObj["features"]["coordinates"].ToObject<Vector2d[]>();
        }


        if (Input.GetKeyDown(KeyCode.O))
        {
            string filePath = Application.persistentDataPath + "/pins.json";
            print("saving to: " + filePath);
            JObject jObject = new JObject();
            jObject.Add("componentName", GetType().ToString());



            JObject jDataObject = new JObject();
            jObject.Add("features", jDataObject);

            //jDataObject.Add("_name", );
            jDataObject.Add("totalPinsNumber", pNumber);

            JArray jFriendsArray = JArray.FromObject(positions);
            jDataObject.Add("coordinates", jFriendsArray);

            StreamWriter sw = new StreamWriter(filePath);
            sw.WriteLine(jObject.ToString());
            sw.Close();
        }

    }

}

