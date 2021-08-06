using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, start_pos;
    private GameObject cam;
    public float parallax;

    void Start()
    {
        start_pos = transform.position.x;
        Debug.Log(start_pos);
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        cam = GameObject.Find("Camera");
    }

    // Update is called once per frame
    void Update()
    {
        float temp = cam.transform.position.x * (1 - parallax);
        float dist = cam.transform.position.x * parallax;
        transform.position = new Vector3(start_pos + dist, transform.position.y, transform.position.z);
    }
}
