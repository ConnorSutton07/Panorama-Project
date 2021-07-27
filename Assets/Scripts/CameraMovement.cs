using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float width;
    float speed;
    GameObject Precipitation;

    private void Start()
    {
        Precipitation = GameObject.Find("Precipitation");
        Camera camera = gameObject.GetComponent<Camera>();
        width = camera.aspect * camera.orthographicSize;
        speed = width / 250;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            float NewX = Mathf.Clamp(transform.position.x + speed, Root.LEFT_EDGE + width, Root.RIGHT_EDGE - width);
            transform.position = new Vector3(NewX, transform.position.y, transform.position.z);
            Precipitation.transform.position = new Vector3(NewX, Precipitation.transform.position.y, Precipitation.transform.position.z);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            float NewX = Mathf.Clamp(transform.position.x - speed, Root.LEFT_EDGE + width, Root.RIGHT_EDGE - width);
            transform.position = new Vector3(NewX, transform.position.y, transform.position.z);
            Precipitation.transform.position = new Vector3(NewX, Precipitation.transform.position.y, Precipitation.transform.position.z);
        }
    }
}