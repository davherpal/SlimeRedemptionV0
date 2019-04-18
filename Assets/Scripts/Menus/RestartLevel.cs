using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevel : MonoBehaviour
{
 
    public static RestartLevel instance;

    // VARIABLE NEED TO KNOW IF RETRY GAME IS NEEDED
    public bool retry = false;


    public float maxAltura;
    public int enemiesKilled;
    public int totalScore;

    private void Awake()
    {
          
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                DestroyImmediate(gameObject);
        }     
        
    }

}
