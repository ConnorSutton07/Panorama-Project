using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class SpeciesInteraction : MonoBehaviour
{
    GameObject SpeciesInfo;

    private void Start()
    {
        SpeciesInfo = GameObject.Find("SpeciesInfo");
        SpeciesInfo.SetActive(false);// = false;
    }

    void OnMouseEnter()
    {
        Root.Action.SetPointer();
        SpeciesInfo.SetActive(true);
    }

    void OnMouseExit()
    {
        Root.Action.SetArrow();
        SpeciesInfo.SetActive(false);
    }

    void OnMouseDown()
    {
        Debug.Log("Click");
        
    }
}
