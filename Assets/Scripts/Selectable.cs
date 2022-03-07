using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Selectable : MonoBehaviour
{
    public UnityEvent OnEnter = new UnityEvent();
    public UnityEvent OnExit = new UnityEvent();
    public UnityEvent OnSelect = new UnityEvent();
    public UnityEvent OnDeselect = new UnityEvent();
    [SerializeField]
    protected AudioPlayer player;

    protected virtual void Awake()
    {
        OnEnter.AddListener(onEnter);
        OnExit.AddListener(onExit);
        OnSelect.AddListener(onSelect);
        OnDeselect.AddListener(onDeselect);
    }

    protected virtual void onEnter(){ player.PlaySoundHit(0); }
    protected virtual void onExit(){}
    protected virtual void onSelect(){ player.PlaySoundHit(1); }
    protected virtual void onDeselect(){}
}
