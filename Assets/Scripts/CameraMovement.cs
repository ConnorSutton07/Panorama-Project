using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float width;
    float speed;

    private void Start()
    {
        Camera camera = gameObject.GetComponent<Camera>();
        width = camera.aspect * camera.orthographicSize;
        speed = width / 250;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            transform.position = new Vector3(Mathf.Clamp(transform.position.x + speed, Constants.LEFT_EDGE + width, Constants.RIGHT_EDGE - width), transform.position.y, transform.position.z);

        //transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            transform.position = new Vector3(Mathf.Clamp(transform.position.x - speed, Constants.LEFT_EDGE + width, Constants.RIGHT_EDGE - width), transform.position.y, transform.position.z);

    }
}