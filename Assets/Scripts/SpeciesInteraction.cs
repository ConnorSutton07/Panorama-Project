using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class SpeciesInteraction : MonoBehaviour
{
    void OnMouseEnter()
    {
        Root.Action.SetPointer();
    }

    void OnMouseExit()
    {
        Root.Action.SetArrow();
    }

    void OnMouseDown()
    {
        Debug.Log("Click");
    }
}
