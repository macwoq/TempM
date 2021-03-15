using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Mapbox.Json;

public class WriteJson : MonoBehaviour
{
    public Geometry geometry1;
    public Root root;
    public Feature feature;
    public Properties properties1;


    public string file = "Data.json";

    public void Save()
    {
        string json = JsonUtility.ToJson(root);
        WriteToFile(file, json);
    }

    public void Load()
    {
        //data = new Geometry();
        string json = Read(file);
        JsonUtility.FromJsonOverwrite(json, root);
    }

    private void WriteToFile(string fileName, string json)
    {
        string path = GetFilePath(fileName);
        FileStream fs = new FileStream(path, FileMode.Create);

        using(StreamWriter writer = new StreamWriter(fs))
        {
            writer.Write(json);
        }
    }


    public string Read(string fileName)
    {
        string path = GetFilePath(fileName);
        if (File.Exists(path))
        {
            using(StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }

        }
        else
        {
            Debug.Log("404");
        }

        return "";
    }

    string GetFilePath(string file)
    {
        return Application.persistentDataPath + "/new.json" + file;
    }

}
[System.Serializable]
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
public class Properties
{
}
[System.Serializable]
public class Geometry
{
    public string type;
    public List<double> coordinates;
}
[System.Serializable]
public class Feature
{
    public string type;
    public Properties properties;
    public Geometry geometry;
}
[System.Serializable]
public class Root
{
    public string type;
    public List<Feature> features;
}


