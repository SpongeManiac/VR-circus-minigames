using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Selectable : MonoBehaviour
{
    public UnityEvent<VRHand> OnEnter = new UnityEvent<VRHand>();
    public UnityEvent<VRHand> OnExit = new UnityEvent<VRHand> ();
    public UnityEvent<VRHand> OnSelectIn = new UnityEvent<VRHand> ();
    public UnityEvent<VRHand> OnSelectOut = new UnityEvent<VRHand> ();
    public UnityEvent<VRHand> OnDeselect = new UnityEvent<VRHand> ();
    [SerializeField]
    protected GameObject player;
    [SerializeField]
    protected bool isEnabled = true;

    protected virtual void Awake()
    {
        OnEnter.AddListener(onEnter);
        OnExit.AddListener(onExit);
        OnSelectIn.AddListener(onSelectIn);
        OnSelectOut.AddListener(onSelectOut);
        OnDeselect.AddListener(onDeselect);
    }

    protected virtual void onEnter(VRHand hand)
    {

    }

    protected virtual void onExit(VRHand hand)
    {
    
    }

    protected virtual void onSelectIn(VRHand hand)
    {
    
    }

    protected virtual void onSelectOut(VRHand hand)
    {

    }

    protected virtual void onDeselect(VRHand hand)
    {
    
    }

    protected virtual void Toggle()
    {
        if (isEnabled)
        {
            Disable();
        }
        else
        {
            Enable();
        }
    }

    protected virtual void Enable()
    {
        isEnabled = true;
    }

    protected virtual void Disable()
    {
        isEnabled = false;
    }
}
