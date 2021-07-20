using System;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    void Start()
    {
        Debug.Log("EEEE");
        int bottom_right = ((int)Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x) - 1;
        int bottom_left =  ((int)Camera.main.ScreenToWorldPoint(Vector3.zero).x) - 1;
        
        for (int i = bottom_left; i < bottom_right; i++)
        {
            Debug.Log(i);
        }
        //Camera.main.transform.position = new Vector3(LENGTH / 2, LENGTH / 2, -LENGTH);
    }
    
    void Update ()
    {
        
    }

}