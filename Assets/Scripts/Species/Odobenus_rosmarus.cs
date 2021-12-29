using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Odobenus_rosmarus : MonoBehaviour
{
    float yPos;
    float yCurrent;
    float t;
    public float speed;
    public float amplitude;
    // Start is called before the first frame update
    void Start()
    {
        yPos = transform.position.y;
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, yPos + (amplitude * Mathf.Sin(t)));
        t += speed;
    }
}
