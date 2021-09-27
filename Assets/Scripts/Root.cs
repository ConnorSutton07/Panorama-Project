using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    public static Root Action;

    //-------------
    //   Cursor
    //-------------
    public Texture2D Pointer;
    public Texture2D Arrow;

    private Vector2 PointerOffset;
    private Vector2 ArrowOffset;

    //------------
    //    Map 
    //------------
    public static int LEFT_EDGE = -30;
    public static int RIGHT_EDGE = 50;
    public static int PRECIPITATION_OFFSET = 8;

    private void Start()
    {
        if (Action != null)
            GameObject.Destroy(Action);
        else
        {
            Action = this;
            Action.PointerOffset = new Vector2(Pointer.width / 2, Pointer.height / 2);
            Action.ArrowOffset = new Vector2(Arrow.width / 2, Arrow.height / 2);
            Action.SetArrow();
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void SetPointer()
    {
        Cursor.SetCursor(Pointer, PointerOffset, CursorMode.Auto);
    }

    public void SetArrow()
    {
        Cursor.SetCursor(Arrow, ArrowOffset, CursorMode.Auto);
    }
}
