using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, start_pos;
    public GameObject cam;
    public float parallax;

    void Start()
    {
        start_pos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = cam.transform.position.x * parallax;
        transform.position = new Vector3(start_pos + dist, transform.position.y, transform.position.z);
    }
}
