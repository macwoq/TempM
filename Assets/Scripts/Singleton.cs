using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton <T> : MonoBehaviour where T :MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            FindObjectOfType<T>();
            if(instance == null)
            {
                Debug.Log("cannot find " + typeof(T));
            }

            return instance;
        }

        
    }

}
