using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandUIItem : HandUI
{
    public Button btn;

    protected override void onEnter(VRHand hand)
    {
        btn.image.color = btn.colors.highlightedColor;
    }

    protected override void onExit(VRHand hand)
    {
        btn.image.color = btn.colors.normalColor;
    }

    protected override void onSelect(VRHand hand)
    {
        btn.image.color = btn.colors.pressedColor;
    }

    protected override void onDeselect(VRHand hand)
    {
        btn.image.color = btn.colors.normalColor;
    }
}
