using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRButton : Selectable
{
    //button events
    public UnityEvent OnReleaseIn = new UnityEvent();

    [SerializeField]
    protected Animator animator;
    //selection settings
    [SerializeField]
    protected RGBDemo colorizer;
    [SerializeField]
    protected List<Component> colors = new List<Component>();//first component is the shader color
    

    protected override void Awake()
    {
        base.Awake();
        OnReleaseIn.AddListener(onBtnReleaseIn);
    }

    protected override void onEnter(VRHand hand)
    {
        base.onEnter(hand);
        foreach (Component c in colors)
        {
            colorizer.AddComponent(c);
        }
        colorizer.paused = false;
        if (colors[0].GetType() == typeof(ShaderColor))
        {
            ((ShaderColor)colors[0]).outlineWidth = 0.008f;
        }
        
    }

    protected override void onExit(VRHand hand)
    {
        onDeselect(hand);
        colorizer.paused = true;
        foreach (Component c in colors)
        {
            StartCoroutine("RemoveColor", c);
        }
        if (colors[0].GetType() == typeof(ShaderColor))
        {
            ((ShaderColor)colors[0]).outlineWidth = 0;
        }
    }

    protected virtual IEnumerator RemoveColor(Component c)
    {
        //Debug.Log("Disabling Component: "+c);
        yield return new WaitForEndOfFrame();
        colorizer.RemoveComponent(c);
    }

    protected override void onSelect(VRHand hand)
    {
        base.onSelect(hand);
        Debug.Log("Button selected");
        animator.SetBool("ButtonPressed", true);
    }

    protected virtual void onBtnReleaseIn()
    {
        Debug.Log("Button used successfully");
        animator.SetBool("ButtonPressed", false);
    }

    protected override void onDeselect(VRHand hand)
    {
        Debug.Log("Button cancelled");
        animator.SetBool("ButtonPressed", false);
    }
}
