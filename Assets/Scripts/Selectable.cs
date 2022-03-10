using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Selectable : MonoBehaviour
{
    public UnityEvent<VRHand> OnEnter = new UnityEvent<VRHand>();
    public UnityEvent<VRHand> OnExit = new UnityEvent<VRHand> ();
    public UnityEvent<VRHand> OnSelect = new UnityEvent<VRHand> ();
    public UnityEvent<VRHand> OnDeselect = new UnityEvent<VRHand> ();
    [SerializeField]
    protected AudioPlayer player;

    protected virtual void Awake()
    {
        OnEnter.AddListener(onEnter);
        OnExit.AddListener(onExit);
        OnSelect.AddListener(onSelect);
        OnDeselect.AddListener(onDeselect);
    }

    protected virtual void onEnter(VRHand hand)
    {

    }

    protected virtual void onExit(VRHand hand)
    {
    
    }

    protected virtual void onSelect(VRHand hand)
    {
    
    }

    protected virtual void onDeselect(VRHand hand)
    {
    
    }

}
