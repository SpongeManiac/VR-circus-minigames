using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUIItem : Selectable
{
    //pointer
    public GameObject cursor;
    //pointing
    bool hovering = false;

    private void Update()
    {
        if (hovering)
        {
            //move and show cursor
            UpdateCursor();
        }
        else
        {
            //hide cursor
            HideCursor();
        }
    }

    private void OnEnable()
    {
        //take over controls 
    }

    public void UpdateCursor()
    {

    }

    public void HideCursor()
    {
        
    }
}
