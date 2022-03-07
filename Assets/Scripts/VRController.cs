using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRController : MonoBehaviour
{
    public OVRInput.Controller controller;

    [SerializeField]
    protected Animator animator;

    //Controller events

    //Index Trigger
    public UnityEvent triggerTouchIn = new UnityEvent();
    public UnityEvent triggerTouchOut = new UnityEvent();
    public UnityEvent triggerTouch = new UnityEvent();
    public UnityEvent triggerPressIn = new UnityEvent();
    public UnityEvent triggerPressOut = new UnityEvent();
    public UnityEvent triggerPress = new UnityEvent();
    public UnityEvent triggerTap = new UnityEvent();

    public bool triggerIsTouched { get { var tf = OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, controller); if (tf) { Debug.Log("Trigger is touched right now."); } return tf; } }
    public bool triggerIsPressed { get { var tf = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller) > 0; if (tf) { Debug.Log("Trigger is pressed right now."); } return tf; } }
    public bool triggerPressed { get { var pressed = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller); if (pressed) { Debug.Log("Trigger was pressed this frame."); }  return pressed; } }
    public bool triggerUnpressed { get { var unpressed = OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, controller); if (unpressed) { Debug.Log("Trigger was unpressed this frame."); } return unpressed; } }
    public bool triggerTouched { get { var touched = OVRInput.GetDown(OVRInput.Touch.PrimaryIndexTrigger, controller); if (touched) { Debug.Log("Trigger was touched this frame."); } return touched; } }
    public bool triggerUntouched { get { var untouched = OVRInput.GetUp(OVRInput.Touch.PrimaryIndexTrigger, controller); if (untouched) { Debug.Log("Trigger was untouched this frame: " + untouched); } return untouched; } }
    [SerializeField]
    protected int triggerTapTime = 40;


    //Grip Trigger

    //grip has no touch
    public UnityEvent gripPressIn = new UnityEvent();
    public UnityEvent gripPressOut = new UnityEvent();
    public UnityEvent gripPress = new UnityEvent();
    public UnityEvent gripTap = new UnityEvent();

    public bool gripIsPressed { get { var tf = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0; if (tf) { Debug.Log("Grip is pressed right now."); } return tf; } }
    public bool gripPressed { get { var pressed = OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controller); if (pressed) { Debug.Log("Grip was pressed this frame."); } return pressed; } }
    public bool gripUnpressed { get { var unpressed = OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controller); if (unpressed) { Debug.Log("Grip was unpressed this frame."); } return unpressed; } }
    [SerializeField]
    protected int gripTapTime = 40;


    //A Button
    public UnityEvent aTouchIn = new UnityEvent();
    public UnityEvent aTouchOut = new UnityEvent();
    public UnityEvent aTouch = new UnityEvent();
    public UnityEvent aPressIn = new UnityEvent();
    public UnityEvent aPressOut = new UnityEvent();
    public UnityEvent aPress = new UnityEvent();
    public UnityEvent aTap = new UnityEvent();

    public bool aIsTouched { get { var tf = OVRInput.Get(OVRInput.Touch.One, controller); if (tf) { Debug.Log("A is touched right now."); } return tf; } }
    public bool aIsPressed { get { var tf = OVRInput.Get(OVRInput.Button.One, controller); if (tf) { Debug.Log("A is pressed right now."); } return tf; } }
    public bool aPressed { get { var pressed = OVRInput.GetDown(OVRInput.Button.One, controller); if (pressed) { Debug.Log("A was pressed this frame."); } return pressed; } }
    public bool aUnpressed { get { var unpressed = OVRInput.GetUp(OVRInput.Button.One, controller); if (unpressed) { Debug.Log("A was unpressed this frame."); } return unpressed; } }
    public bool aTouched { get { var touched = OVRInput.GetDown(OVRInput.Touch.One, controller); if (touched) { Debug.Log("A was touched this frame."); } return touched; } }
    public bool aUntouched { get { var untouched = OVRInput.GetUp(OVRInput.Touch.One, controller); if (untouched) { Debug.Log("A was untouched this frame."); } return untouched; } }
    [SerializeField]
    protected int aTapTime = 40;

    //B Button
    public UnityEvent bTouchIn = new UnityEvent();
    public UnityEvent bTouchOut = new UnityEvent();
    public UnityEvent bTouch = new UnityEvent();
    public UnityEvent bPressIn = new UnityEvent();
    public UnityEvent bPressOut = new UnityEvent();
    public UnityEvent bPress = new UnityEvent();
    public UnityEvent bTbp = new UnityEvent();

    public bool bIsTouched { get { var tf = OVRInput.Get(OVRInput.Touch.Two, controller); if (tf) { Debug.Log("B is touched right now."); } return tf; } }
    public bool bIsPressed { get { var tf = OVRInput.Get(OVRInput.Button.Two, controller); if (tf) { Debug.Log("B is pressed right now."); } return tf; } }
    public bool bPressed { get { var pressed = OVRInput.GetDown(OVRInput.Button.Two, controller); if (pressed) { Debug.Log("B was pressed this frame."); } return pressed; } }
    public bool bUnpressed { get { var unpressed = OVRInput.GetUp(OVRInput.Button.Two, controller); if (unpressed) { Debug.Log("B was unpressed this frame."); } return unpressed; } }
    public bool bTouched { get { var touched = OVRInput.GetDown(OVRInput.Touch.Two, controller); if (touched) { Debug.Log("B was touched this frame."); } return touched; } }
    public bool bUntouched { get { var untouched = OVRInput.GetUp(OVRInput.Touch.Two, controller); if (untouched) { Debug.Log("B was untouched this frame."); } return untouched; } }
    [SerializeField]
    protected int bTapTime = 40;

    //Menu button
    public UnityEvent menuPressIn = new UnityEvent();
    public UnityEvent menuPressOut = new UnityEvent();
    public UnityEvent menuPress = new UnityEvent();
    public UnityEvent menuTmenup = new UnityEvent();


    //menu has no touch
    public bool menuIsPressed { get { var tf = OVRInput.Get(OVRInput.Button.Start, controller); if (tf) { Debug.Log("menu is pressed right now."); } return tf; } }
    public bool menuPressed { get { var pressed = OVRInput.GetDown(OVRInput.Button.Start, controller); if (pressed) { Debug.Log("Menu was pressed this frame."); } return pressed; } }
    public bool menuUnpressed { get { var unpressed = OVRInput.GetUp(OVRInput.Button.Start, controller); if (unpressed) { Debug.Log("Menu was unpressed this frame."); } return unpressed; } }
    [SerializeField]
    protected int menuTapTime = 40;

    //ThumbStick
    public UnityEvent thumbTouchIn = new UnityEvent();
    public UnityEvent thumbTouchOut = new UnityEvent();
    public UnityEvent thumbTouch = new UnityEvent();
    public UnityEvent thumbPressIn = new UnityEvent();
    public UnityEvent thumbPressOut = new UnityEvent();
    public UnityEvent thumbPress = new UnityEvent();
    public UnityEvent thumbTap = new UnityEvent();

    public bool thumbIsTouched { get { var tf = OVRInput.Get(OVRInput.Touch.PrimaryThumbstick, controller); if (tf) { Debug.Log("Thumbstick is touched right now."); } return tf; } }
    public bool thumbIsPressed { get { var tf = OVRInput.Get(OVRInput.Button.PrimaryThumbstick, controller); if (tf) { Debug.Log("Thumbstick is pressed right now."); } return tf; } }
    public bool thumbPressed { get { var pressed = OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, controller); if (pressed) { Debug.Log("Thumbstick was pressed this frame."); } return pressed; } }
    public bool thumbUnpressed { get { var unpressed = OVRInput.GetUp(OVRInput.Button.PrimaryThumbstick, controller); if (unpressed) { Debug.Log("Thumbstick was unpressed this frame."); } return unpressed; } }
    public bool thumbTouched { get { var touched = OVRInput.GetDown(OVRInput.Touch.PrimaryThumbstick, controller); if (touched) { Debug.Log("Thumbstick was touched this frame."); } return touched; } }
    public bool thumbUntouched { get { var untouched = OVRInput.GetUp(OVRInput.Touch.PrimaryThumbstick, controller); if (untouched) { Debug.Log("Thumbstick was untouched this frame."); } return untouched; } }
    [SerializeField]
    protected int thumbTapTime = 40;

    //timing
    int triggerTime = 0;
    int gripTime = 0;
    int aTime = 0;
    int bTime = 0;
    int menuTime = 0;
    int thumbTime = 0;

    //Update is called once per frame
    void Update()
    {
        // Trigger
        if (triggerIsTouched) { triggerTouch.Invoke(); }
        if (triggerIsPressed) { triggerTime++; /*Debug.Log(triggerTime);*/ triggerPress.Invoke(); }
        if (triggerTouched) { triggerTouchIn.Invoke(); }
        if (triggerUntouched) { triggerTouchOut.Invoke(); }
        if (triggerPressed) { triggerPressIn.Invoke(); }
        if (triggerUnpressed) { triggerPressOut.Invoke(); if (triggerTime <= triggerTapTime) { Debug.Log("Trigger tapped!"); triggerTap.Invoke(); } triggerTime = 0; }

        // Grip
        if (gripIsPressed) { gripTime++; gripPress.Invoke(); }
        if (gripPressed) { gripPressIn.Invoke(); }
        if (gripUnpressed) { gripPressOut.Invoke(); if (gripTime <= gripTapTime) { gripTap.Invoke(); } gripTime = 0; }

        // A Button
        if (aIsTouched) { aTouch.Invoke(); }
        if (aIsPressed) { aTime++;  aPress.Invoke(); }
        if (aTouched) { aTouchIn.Invoke(); }
        if (aUntouched) { aTouchOut.Invoke(); }
        if (aPressed) { aPressIn.Invoke(); }
        if (aUnpressed) { aPressOut.Invoke(); if (aTime <= aTapTime) { aTap.Invoke(); } aTime = 0; }

        //B Button
        if (bIsTouched) { bTouch.Invoke(); }
        if (bIsPressed) { bTime++;  aPress.Invoke(); }
        if (bTouched) { bTouchIn.Invoke(); }
        if (bUntouched) { bTouchOut.Invoke(); }
        if (bPressed) { bPressOut.Invoke(); }
        if (bUnpressed) { bPressOut.Invoke(); if (bTime <= bTapTime) { bPressOut.Invoke(); } bTime = 0; }

        //Menu Button
        if (menuIsPressed) { menuTime++; menuPress.Invoke(); }
        if (menuPressed) { menuPressOut.Invoke(); }
        if (menuUnpressed) { menuPressOut.Invoke(); if (menuTime <= menuTapTime) { menuPressOut.Invoke(); } menuTime = 0; }

        //Thumbstick
        if (thumbIsTouched) { thumbTouch.Invoke(); }
        if (thumbIsPressed) { thumbTime++; aPress.Invoke(); }
        if (thumbTouched) { thumbTouchIn.Invoke(); }
        if (thumbUntouched) { thumbTouchOut.Invoke(); }
        if (thumbPressed) { thumbPressOut.Invoke(); }
        if (thumbUnpressed) { thumbPressOut.Invoke(); if (thumbTime <= thumbTapTime) { thumbPressOut.Invoke(); } thumbTime = 0; }
    }
}
