using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandUIItem : HandUI
{
    public Button btn;


    protected override void onEnter(VRHand hand)
    {
        if (isEnabled)
        {
            btn.image.color = btn.colors.highlightedColor;
        }
    }

    protected override void onExit(VRHand hand)
    {
        if (isEnabled)
        {
            btn.image.color = btn.colors.normalColor;
        }
    }

    protected override void onSelectIn(VRHand hand)
    {
        if (isEnabled)
        {
            btn.image.color = btn.colors.pressedColor;
        }
    }
    protected override void onSelectOut(VRHand hand)
    {
        if (isEnabled)
        {
            onEnter(hand);
        }
    }

    protected override void onDeselect(VRHand hand)
    {
        if (isEnabled)
        {
            btn.image.color = btn.colors.normalColor;
        }
    }

    protected virtual void ToggleButton()
    {
        btn.enabled = !btn.enabled;
    }
    protected override void Enable()
    {
        base.Enable();
        btn.image.color = btn.colors.normalColor;
    }

    protected override void Disable()
    {
        base.Disable();
        btn.image.color = btn.colors.disabledColor;
    }

}
