using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] bool allowVertical;
    float width;
    float speed;

    private void Start()
    {
        Camera camera = gameObject.GetComponent<Camera>();
        width = camera.aspect * camera.orthographicSize;
        speed = width / 50;
    }

    void FixedUpdate()
    {
        float xMovement = 0;
        float yMovement = 0;

        if (MoveRight())
            xMovement += speed;
        if (MoveLeft())
            xMovement -= speed;
        if (MoveUp())
            yMovement += speed;
        if (MoveDown())
            yMovement -= speed;

        transform.position = new Vector3(transform.position.x + xMovement, transform.position.y + yMovement, transform.position.z);
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
