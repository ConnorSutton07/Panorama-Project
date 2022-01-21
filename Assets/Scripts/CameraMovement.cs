using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] bool allowVertical;
    [SerializeField] bool includePrecipitation;
    [SerializeField] float precipitationOffset;
    float width;
    float speed;
    GameObject Precipitation;

    private void Start()
    {
        Precipitation = GameObject.Find("Precipitation");
        Camera camera = gameObject.GetComponent<Camera>();
        width = camera.aspect * camera.orthographicSize;
        speed = width / 50;
    }

    void FixedUpdate()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if (MoveRight() && ! MoveLeft())
            x = Mathf.Clamp(transform.position.x + speed, Root.LEFT_EDGE + width, Root.RIGHT_EDGE - width);
        else if (MoveLeft())
            x = Mathf.Clamp(transform.position.x - speed, Root.LEFT_EDGE + width, Root.RIGHT_EDGE - width);
        if (MoveUp() && !MoveDown())
            y = transform.position.y + speed;
        else if (MoveDown())
            y = transform.position.y - speed;

        transform.position = new Vector3(x, y, transform.position.z);
    }

    bool MoveRight()
    {
        return Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
    }

    bool MoveLeft()
    {
        return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
    }

    bool MoveUp()
    {
        return Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
    }

    bool MoveDown()
    {
        return Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
    }
}
