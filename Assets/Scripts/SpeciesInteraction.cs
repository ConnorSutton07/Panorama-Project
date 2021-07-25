using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class SpeciesInteraction : MonoBehaviour
{
    enum WindowsCursor
    {
        StandardArrow = 32512,
        Hand = 32649
    }

    private static void ChangeCursor(WindowsCursor cursor)
    {
        SetCursor(LoadCursor(IntPtr.Zero, (int)cursor));
    }

    [DllImport("user32.dll", EntryPoint = "SetCursor")]
    public static extern IntPtr SetCursor(IntPtr hCursor);

    [DllImport("user32.dll", EntryPoint = "LoadCursor")]
    public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

    void OnMouseEnter()
    {
        //Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        ChangeCursor(WindowsCursor.Hand);
        /*if (entered == false)
        {
            ChangeCursor(WindowsCursor.Hand);
            entered = true;
        }
        */
    }

    void OnMouseExit()
    {
        //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        ChangeCursor(WindowsCursor.StandardArrow);
        Debug.Log("Exit");
    }

    void OnMouseDown()
    {
        Debug.Log("Click");
    }
}
