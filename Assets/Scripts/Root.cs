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
    public static int RIGHT_EDGE = 30;
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

    public void SetPointer()
    {
        Debug.Log("Pointer");
        Cursor.SetCursor(Pointer, PointerOffset, CursorMode.Auto);
    }

    public void SetArrow()
    {
        Debug.Log("Arrow");
        Cursor.SetCursor(Arrow, ArrowOffset, CursorMode.Auto);
    }
}
