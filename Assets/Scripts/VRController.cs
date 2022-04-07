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
    public List<UnityEvent> triggerEvents
    {
        get
        {
            return new List<UnityEvent>() {
                triggerTouchIn,
                triggerTouchOut,
                triggerTouch,
                triggerPressIn,
                triggerPressOut,
                triggerPress,
                triggerTap,
            };
        }
    }

    public UnityEvent triggerTouchIn = new UnityEvent();
    public UnityEvent triggerTouchOut = new UnityEvent();
    public UnityEvent triggerTouch = new UnityEvent();
    public UnityEvent triggerPressIn = new UnityEvent();
    public UnityEvent triggerPressOut = new UnityEvent();
    public UnityEvent triggerPress = new UnityEvent();
    public UnityEvent triggerTap = new UnityEvent();

    public bool triggerTIn { get { var touched = OVRInput.GetDown(OVRInput.Touch.PrimaryIndexTrigger, controller); if (touched) { Debug.Log("Trigger was touched this frame."); } return touched; } }
    public bool triggerTOut { get { var untouched = OVRInput.GetUp(OVRInput.Touch.PrimaryIndexTrigger, controller); if (untouched) { Debug.Log("Trigger was untouched this frame: " + untouched); } return untouched; } }
    public bool triggerIsTouched { get { var tf = OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, controller); if (tf) { Debug.Log("Trigger is touched right now."); } return tf; } }
    public bool triggerIn { get { var pressed = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller); if (pressed) { Debug.Log("Trigger was pressed this frame."); } return pressed; } }
    public bool triggerOut { get { var unpressed = OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, controller); if (unpressed) { Debug.Log("Trigger was unpressed this frame."); } return unpressed; } }
    public bool triggerIsPressed { get { var tf = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller) > 0; if (tf) { Debug.Log("Trigger is pressed right now."); } return tf; } }
    [SerializeField]
    protected int triggerTapTime = 40;


    //Grip Trigger
    //grip has no touch
    public List<UnityEvent> gripEvents
    {
        get
        {
            return new List<UnityEvent>() {
                gripPressIn,
                gripPressOut,
                gripPress,
                gripTap,
            };
        }
    }
    public UnityEvent gripPressIn = new UnityEvent();
    public UnityEvent gripPressOut = new UnityEvent();
    public UnityEvent gripPress = new UnityEvent();
    public UnityEvent gripTap = new UnityEvent();

    public bool gripIsPressed { get { var tf = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0; if (tf) { Debug.Log("Grip is pressed right now."); } return tf; } }
    public bool gripIn { get { var pressed = OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controller); if (pressed) { Debug.Log("Grip was pressed this frame."); } return pressed; } }
    public bool gripOut { get { var unpressed = OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controller); if (unpressed) { Debug.Log("Grip was unpressed this frame."); } return unpressed; } }
    [SerializeField]
    protected int gripTapTime = 40;


    //A Button
    public List<UnityEvent> aEvents
    {
        get
        {
            return new List<UnityEvent>() {
                aTouchIn,
                aTouchOut,
                aTouch,
                aPressIn,
                aPressOut,
                aPress,
                aTap,
            };
        }
    }

    public UnityEvent aTouchIn = new UnityEvent();
    public UnityEvent aTouchOut = new UnityEvent();
    public UnityEvent aTouch = new UnityEvent();
    public UnityEvent aPressIn = new UnityEvent();
    public UnityEvent aPressOut = new UnityEvent();
    public UnityEvent aPress = new UnityEvent();
    public UnityEvent aTap = new UnityEvent();

    public bool aTIn { get { var touched = OVRInput.GetDown(OVRInput.Touch.One, controller); if (touched) { Debug.Log("A was touched this frame."); } return touched; } }
    public bool aTOut { get { var untouched = OVRInput.GetUp(OVRInput.Touch.One, controller); if (untouched) { Debug.Log("A was untouched this frame."); } return untouched; } }
    public bool aIsTouched { get { var tf = OVRInput.Get(OVRInput.Touch.One, controller); if (tf) { Debug.Log("A is touched right now."); } return tf; } }
    public bool aIn { get { var pressed = OVRInput.GetDown(OVRInput.Button.One, controller); if (pressed) { Debug.Log("A was pressed this frame."); } return pressed; } }
    public bool aOut { get { var unpressed = OVRInput.GetUp(OVRInput.Button.One, controller); if (unpressed) { Debug.Log("A was unpressed this frame."); } return unpressed; } }
    public bool aIsPressed { get { var tf = OVRInput.Get(OVRInput.Button.One, controller); if (tf) { Debug.Log("A is pressed right now."); } return tf; } }
    [SerializeField]
    protected int aTapTime = 40;

    //B Button
    public List<UnityEvent> bEvents
    {
        get
        {
            return new List<UnityEvent>() {
                bTouchIn,
                bTouchOut,
                bTouch,
                bPressIn,
                bPressOut,
                bPress,
                bTap,
            };
        }
    }

    public UnityEvent bTouchIn = new UnityEvent();
    public UnityEvent bTouchOut = new UnityEvent();
    public UnityEvent bTouch = new UnityEvent();
    public UnityEvent bPressIn = new UnityEvent();
    public UnityEvent bPressOut = new UnityEvent();
    public UnityEvent bPress = new UnityEvent();
    public UnityEvent bTap = new UnityEvent();

    public bool bTIn { get { var touched = OVRInput.GetDown(OVRInput.Touch.Two, controller); if (touched) { Debug.Log("B was touched this frame."); } return touched; } }
    public bool bTOut { get { var untouched = OVRInput.GetUp(OVRInput.Touch.Two, controller); if (untouched) { Debug.Log("B was untouched this frame."); } return untouched; } }
    public bool bIsTouched { get { var tf = OVRInput.Get(OVRInput.Touch.Two, controller); if (tf) { Debug.Log("B is touched right now."); } return tf; } }
    public bool bIn { get { var pressed = OVRInput.GetDown(OVRInput.Button.Two, controller); if (pressed) { Debug.Log("B was pressed this frame."); } return pressed; } }
    public bool bOut { get { var unpressed = OVRInput.GetUp(OVRInput.Button.Two, controller); if (unpressed) { Debug.Log("B was unpressed this frame."); } return unpressed; } }
    public bool bIsPressed { get { var tf = OVRInput.Get(OVRInput.Button.Two, controller); if (tf) { Debug.Log("B is pressed right now."); } return tf; } }
    [SerializeField]
    protected int bTapTime = 40;

    //Menu button
    //menu has no touch
    public List<UnityEvent> menuEvents
    {
        get
        {
            return new List<UnityEvent>() {
                menuPressIn,
                menuPressOut,
                menuPress,
                menuTap,
            };
        }
    }

    public UnityEvent menuPressIn = new UnityEvent();
    public UnityEvent menuPressOut = new UnityEvent();
    public UnityEvent menuPress = new UnityEvent();
    public UnityEvent menuTap = new UnityEvent();


    public bool menuIsPressed
    {
        get
        {
            var tf = OVRInput.Get(OVRInput.Button.Start, controller);
            if (tf)
            {
                Debug.Log("menu is pressed right now.");
            }
            return tf;
        }
    }
    public bool menuIn { get { var pressed = OVRInput.GetDown(OVRInput.Button.Start, controller); if (pressed) { Debug.Log("Menu was pressed this frame."); } return pressed; } }
    public bool menuOut { get { var unpressed = OVRInput.GetUp(OVRInput.Button.Start, controller); if (unpressed) { Debug.Log("Menu was unpressed this frame."); } return unpressed; } }
    [SerializeField]
    protected int menuTapTime = 40;

    //ThumbStick
    public List<UnityEvent> thumbEvents
    {
        get
        {
            return new List<UnityEvent>() {
                thumbTouchIn,
                thumbTouchOut,
                thumbTouch,
                thumbPressIn,
                thumbPressOut,
                thumbPress,
                thumbTap,
            };
        }
    }

    public UnityEvent thumbTouchIn = new UnityEvent();
    public UnityEvent thumbTouchOut = new UnityEvent();
    public UnityEvent thumbTouch = new UnityEvent();
    public UnityEvent thumbPressIn = new UnityEvent();
    public UnityEvent thumbPressOut = new UnityEvent();
    public UnityEvent thumbPress = new UnityEvent();
    public UnityEvent thumbTap = new UnityEvent();

    public bool thumbTIn { get { var touched = OVRInput.GetDown(OVRInput.Touch.PrimaryThumbstick, controller); if (touched) { Debug.Log("Thumbstick was touched this frame."); } return touched; } }
    public bool thumbTOut { get { var untouched = OVRInput.GetUp(OVRInput.Touch.PrimaryThumbstick, controller); if (untouched) { Debug.Log("Thumbstick was untouched this frame."); } return untouched; } }
    public bool thumbIsTouched { get { var tf = OVRInput.Get(OVRInput.Touch.PrimaryThumbstick, controller); if (tf) { Debug.Log("Thumbstick is touched right now."); } return tf; } }
    public bool thumbIn { get { var pressed = OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, controller); if (pressed) { Debug.Log("Thumbstick was pressed this frame."); } return pressed; } }
    public bool thumbOut { get { var unpressed = OVRInput.GetUp(OVRInput.Button.PrimaryThumbstick, controller); if (unpressed) { Debug.Log("Thumbstick was unpressed this frame."); } return unpressed; } }
    public bool thumbIsPressed { get { var tf = OVRInput.Get(OVRInput.Button.PrimaryThumbstick, controller); if (tf) { Debug.Log("Thumbstick is pressed right now."); } return tf; } }
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
        if (triggerTIn) { triggerTouchIn.Invoke(); }
        if (triggerTOut) { triggerTouchOut.Invoke(); }
        if (triggerIn) { triggerPressIn.Invoke(); }
        if (triggerOut) { triggerPressOut.Invoke(); if (triggerTime <= triggerTapTime) { Debug.Log("Trigger tapped!"); triggerTap.Invoke(); } triggerTime = 0; }

        // Grip
        if (gripIsPressed) { gripTime++; gripPress.Invoke(); }
        if (gripIn) { gripPressIn.Invoke(); }
        if (gripOut) { gripPressOut.Invoke(); if (gripTime <= gripTapTime) { gripTap.Invoke(); } gripTime = 0; }

        // A Button
        if (aIsTouched) { aTouch.Invoke(); }
        if (aIsPressed) { aTime++; aPress.Invoke(); }
        if (aTIn) { aTouchIn.Invoke(); }
        if (aTOut) { aTouchOut.Invoke(); }
        if (aIn) { aPressIn.Invoke(); }
        if (aOut) { aPressOut.Invoke(); if (aTime <= aTapTime) { aTap.Invoke(); } aTime = 0; }

        //B Button
        if (bIsTouched) { bTouch.Invoke(); }
        if (bIsPressed) { bTime++; aPress.Invoke(); }
        if (bTIn) { bTouchIn.Invoke(); }
        if (bTOut) { bTouchOut.Invoke(); }
        if (bIn) { bPressOut.Invoke(); }
        if (bOut) { bPressOut.Invoke(); if (bTime <= bTapTime) { bPressOut.Invoke(); } bTime = 0; }

        //Menu Button
        if (menuIsPressed) { menuTime++; menuPress.Invoke(); }
        if (menuIn) { menuPressIn.Invoke(); }
        if (menuOut) { menuPressOut.Invoke(); if (menuTime <= menuTapTime) { menuTap.Invoke(); } menuTime = 0; }

        //Thumbstick button
        if (thumbIsTouched) { thumbTouch.Invoke(); }
        if (thumbIsPressed) { thumbTime++; thumbPressIn.Invoke(); }
        if (thumbTIn) { thumbTouchIn.Invoke(); }
        if (thumbTOut) { thumbTouchOut.Invoke(); }
        if (thumbIn) { thumbPressOut.Invoke(); }
        if (thumbOut) { thumbPressOut.Invoke(); if (thumbTime <= thumbTapTime) { thumbPressOut.Invoke(); } thumbTime = 0; }
    }
}
