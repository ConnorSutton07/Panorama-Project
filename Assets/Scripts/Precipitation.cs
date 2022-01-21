using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Precipitation : MonoBehaviour
{
    [SerializeField] float xOffset;
    [SerializeField] Camera cam;
    float width;
    float speed;
    Vector3 cPos;

    // Update is called once per frame
    void FixedUpdate()
    {
        cPos = cam.transform.position;
        transform.position = new Vector3(cPos.x + xOffset, transform.position.y, transform.position.z);        
    }
}
