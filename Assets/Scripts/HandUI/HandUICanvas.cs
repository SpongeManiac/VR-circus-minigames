using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUICanvas : HandUI
{
    //impact point, worldspace
    [SerializeField]
    private RaycastHit impact;

    public Vector2 cursorPos;

    bool hovering = false;

    protected override void onEnter(VRHand hand)
    {
        Debug.Log("Entered canvas!");
    }

    protected override void onExit(VRHand hand)
    {
    }

    protected override void onSelect(VRHand hand)
    {
        Debug.Log("Selected canvas!");
    }

    protected override void onDeselect(VRHand hand)
    {
    }
}
