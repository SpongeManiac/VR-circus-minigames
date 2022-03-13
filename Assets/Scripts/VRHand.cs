using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OVR;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VRHand : MonoBehaviour
{
    //hand settings
    public OVRInput.Controller controllerType { get { return controller.controller; } }
    [SerializeField]
    protected VRHand opposite; //reference to opposite hand
    [SerializeField]
    public VRController controller; //tells the hand which controller controls it
    public Vector3 velocity { get { return _velocity.velocity; } }
    [SerializeField]
    protected Velocity _velocity;
    [SerializeField]
    protected LineRenderer lineRenderer; //the line renderer for showing where user is pointing
    [SerializeField]
    protected GameObject finger;
    [SerializeField]
    protected GameObject fingerTip;
    [SerializeField]
    protected Camera fingerCam;
    public Transform gripOffset { get { return _gripOffset; } } //where grabbed things snap to (the grabbable has an offset too)
    [SerializeField]
    protected Transform _gripOffset; //where grabbed things snap to (the grabbable has an offset too)


    //selection settings
    [SerializeField]
    protected LayerMask selectionMask; //identifies what layer collisions should be detected on
    [SerializeField]
    protected LayerMask UIMask;
    [SerializeField]
    protected float maxDistance; //how far the ray will go

    //selection states
    [SerializeField]
    protected bool selector = false; //whether or not the hand is a selector
    [SerializeField]
    protected int selectorCooldown = 10;
    [SerializeField]
    protected int selectorCount = 0;
    [SerializeField]
    protected bool canSelect = false; //whether or not the hand can select something
    [SerializeField]
    protected bool selecting = false; //whether or not the hand is currently over a selectable

    //selection data
    [SerializeField]
    protected Selectable selectedScript = null; //selected object's script

    //grab states
    [SerializeField]
    protected bool canGrab = true; //whether or not the hand can grab
    [SerializeField]
    protected bool grabbing = false; //whether or not the hand is grabbing
    [SerializeField]
    protected bool grabbablePresent { //whether or not there is a grabbable object in range
        get { return grabbables.Count > 0; }
    }
    [SerializeField]
    protected bool detectingGrab = false; //whether or not the hand is detecting a grab

    //grab data
    [SerializeField]
    protected Grabbable grabbedScript = null; //grabbed object's script
    [SerializeField]
    protected List<Grabbable> grabbables = new List<Grabbable>(); //list of grabbables in range

    //UI state
    [SerializeField]
    protected bool canUseUI = true;
    [SerializeField]
    protected bool overUI = false;
    [SerializeField]
    protected bool pressing = false;

    //Menu UI
    protected bool menuOpen = false;
    [SerializeField]
    protected GameObject MenuUI;

    //UI data
    [SerializeField]
    protected Selectable UIScript = null; //the button that is pointed at
    [SerializeField]
    protected Selectable pressedButton = null; //the button that the press was initiated on
    [SerializeField]
    protected GameObject cursor;

    //movement
    [SerializeField]
    private Transform forward;
    private Vector2 leftStickPos {get => OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);}
    private Vector2 rightStickPos { get => OVRInput.Get(OVRInput.RawAxis2D.RThumbstick); }
    [SerializeField]
    private float maxSpeed = 0.01f;
    [SerializeField]
    private float maxRotationSpeed = 2f;
    [SerializeField]
    private float speedMultiplier = 1.2f;
    [SerializeField]
    private Vector3 desiredVelocity = new Vector3(0f, 0f, 0f);
    [SerializeField]
    private float desiredRotationSpeed = 0f;
    [SerializeField]
    private CharacterController player;


    [SerializeField]
    protected GraphicRaycaster graphicCaster;
    [SerializeField]
    private EventSystem eventSystem;
    //User should be able to grab with both hands at all times.
    //Only one hand can select UI elements at any given time.
    //Selected UI elements are deselected if the hand is no longer a selector.




    private void Awake()
    {
        //add menu button
        controller.menuTap.AddListener(ToggleVRMenu);
        //setup UI selector trigger
        controller.triggerTap.AddListener(MakeSelector); //tell the controller to listen for selector queue
        //setup line renderer
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    void Update()
    {
        //Debug.Log("Hand velocity: " + velocity.velocity);

        if (selector)
        {
            if (!grabbing)
            {
                CheckPointer();
            }
        }
        else
        {
            selectorCount++;
        }

        //Debug.Log("Selected Script: " + selectedScript);
        if (!selecting && canGrab && grabbablePresent && !grabbing && grabbables[0].grabbable) //if we cant grab, dont bother. If there is nothing to grab, dont bother. Finally, if all are true, grab the thing.
        {
            //highlight grabbable
            if (selectedScript != null && selectedScript != grabbables[0])
            {
                Deselect();
            }
            Select(grabbables[0]);
        }


        UpdateMovement();
    }

    //function

    public float Map(float x, float in_min, float in_max, float out_min, float out_max, bool clamp = true)
    {
        if (clamp) x = Mathf.Max(in_min, Mathf.Min(x, in_max));
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    void UpdateMovement()
    {
        Debug.Log("Left Stick Pos: "+leftStickPos);
        //check if this controller is left or right hand
        if (controller.controller == OVRInput.Controller.LTouch)
        {
            //Debug.Log("Hand is left hand");
            //if left stick is not at 0,0 start to move the character
            if (controller.thumbIsTouched &&  (leftStickPos.x >= 0f || leftStickPos.y >= 0f))
            {
                //map from stick pos to speed
                float mappedX = Map(leftStickPos.x, -1f, 1f, -maxSpeed, maxSpeed);
                float mappedY = Map(leftStickPos.y, -1f, 1f, -maxSpeed, maxSpeed);
                //add current rotation to velocity
                
                desiredVelocity = new Vector3(mappedX, mappedY, 0f);// Vector2.Lerp(desiredVelocity, , 0.1f);
                player.SimpleMove(desiredVelocity);
            }
            else
            {
                desiredVelocity = Vector2.Lerp(desiredVelocity, Vector2.zero, 0.2f);
            }
        }
        else
        {
            desiredVelocity = Vector2.zero;
        }
        //check if this controller is right hand
        if (controller.controller == OVRInput.Controller.RHand)
        {
            Debug.Log("Hand is right hand");
            //if the right stick is not at 0,0 move the camera
            if (rightStickPos.x >= 0f || rightStickPos.y >= 0f)
            {

            }
            else
            {

            }
        }
        else
        {

        }

        //set player velocity to desired velocity
        Debug.Log("Desired velocity: "+desiredVelocity);
        //player.SimpleMove(desiredVelocity);
        Debug.Log("Actual velocity: "+ player.velocity);
    }

    void ToggleVRMenu()
    {
        if (menuOpen)
        {
            //close menu
            MenuUI.SetActive(false);
            menuOpen = false;
        }
        else
        {
            //open menu
            MenuUI.SetActive(true);
            menuOpen = true;
        }
        Debug.Log("");
    }
    
    void MakeSelector() //handles everything related to switching this controller to selecting
    {
        Debug.Log("Make selector called!");
        if (selectorCount >= selectorCooldown && !grabbing)
        {
            selectorCount = 0;
            opposite.selectorCount = 0;
            //remove event listener
            controller.triggerTap.RemoveListener(MakeSelector);
            if (opposite.selector)
            {
                //disable whichever activity is active on the opposite hand
                opposite.RevokeSelector();
            }
            selector = true;
            TimedVibrate(1f, 0.25f, 0.1f);
        }
    }

    void RevokeSelector() //removes selector status
    {
        //not a selector anymore
        selector = false;
        //re-enable event listener
        controller.triggerTap.AddListener(MakeSelector);
        if (pressing)
        {
            UIScript.OnExit.Invoke(this); //release the press as if it were cancelled
        }
        if (selecting && selectedScript.GetType() == typeof(VRButton))
        {
            Deselect();
        }
    }

    void EndActivities() //ends all activities the hand might be doing
    {
        if (grabbing)
        {
            Drop();
        }
        if (pressing)
        {
            UIScript.OnExit.Invoke(this);
        }
    }

    //Utility

    void Select(Selectable script) //selects/highlights something
    {
        selectedScript = script;
        Debug.Log("Selected script: "+selectedScript);
        selecting = true;
        TimedVibrate(0.01f, 0.01f, 0.1f);
        script.OnEnter.Invoke(this);
        //set events based on type of selectable
        switch (script)
        {
            case HandUI handUI:
                controller.aPress.AddListener(Use);
                break;
            case VRButton btn:
                //add A listener
                controller.aPress.AddListener(Use);
                break;
            case Grabbable grab:
                //add grab listener
                if (canGrab && grab.grabbable)
                {
                    controller.gripPressIn.AddListener(Grab);
                }
                break;
        }
    }

    void Deselect()
    {
        if (selecting)//ensure that something is selected first
        {
            selecting = false;
            selectedScript.OnExit.Invoke(this);
            switch (selectedScript)
            {
                case HandUI handUI:
                    controller.aPress.RemoveListener(Use);
                    break;
                case VRButton btn:
                    controller.aPressIn.RemoveListener(Use);
                    controller.aPressOut.RemoveListener(Release);
                    break;
                case Grabbable grab:
                    controller.gripPressIn.RemoveListener(Grab);
                    break;
            }
            selectedScript = null;
        }
    }

    void CheckPointer()
    {
        //get finger position and direction towards finger tip
        Vector3 p1 = finger.transform.position;
        Vector3 p2 = (fingerTip.transform.position - p1).normalized;
        //Vector3 p2 = fingerTip.transform.localPosition;
        //create a ray from p1 in the direction of p2
        Ray ray = new Ray(p1, p2);
        //Set the collision filter and ray distance
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, selectionMask))
        {
            if (!overUI)
            {
                overUI = true;
            }
            lineRenderer.SetPositions(new Vector3[] { fingerTip.transform.position, hit.point });
            lineRenderer.enabled = true;
            if (hit.collider.TryGetComponent<Selectable>(out var selectable))
            {
                Debug.Log("Pointing at : "+selectable.gameObject.name);
                //we are pointing at a selectable
                if(selectedScript != null) //if we have anything selected
                {
                    if (selectable != selectedScript)
                        // if what we are pointing at is new, deselect the old and select the new
                    {
                        Deselect();
                        switch (selectable)
                        {
                            case HandUI ui:
                                //create graphics raycast to this location
                                //var graphicsRay = fingerCam;
                                Select(selectable);
                                //update cursor location
                                break;
                            case VRButton btn:
                                Select(selectable);
                                break;
                            case Grabbable grab:
                                if (canGrab && grab.grabbable && grab.distanceGrabbable && hit.distance <= grab.maxDistance)
                                {
                                    Select(selectable);
                                }
                                break;
                        }
                    }
                }
                else //this is the first thing to be selected
                {
                    switch (selectable)
                    {
                        case HandUI ui:
                            Select(selectable);
                            break;
                        case VRButton btn:
                            Select(selectable);
                            break;
                        case Grabbable grab:
                            if (canGrab && grab.grabbable && grab.distanceGrabbable && hit.distance <= grab.maxDistance)
                            {
                                Select(selectable);
                            }
                            break;
                    }
                }
            }
            else//no selectable, deselect current if it exists
            {
                if (selectedScript != null)
                {
                    Deselect();
                }
            }
        }
        else //not pointing at anything interactable
        {
            //deselect current if it exists
            if(selectedScript != null)
            {
                Deselect();
            }
            if (overUI)
            {
                overUI = false;
            }
            //disable line renderer
            lineRenderer.enabled = false;
        }
    }

    private void TimedVibrate(float frequency, float intensity, float time)
    {
        OVRInput.SetControllerVibration(frequency, intensity, controllerType);
        StartCoroutine(EndVibrate(time));
    }
    IEnumerator EndVibrate(float time)
    {
        yield return new WaitForSeconds(time);
        OVRInput.SetControllerVibration(0, 0, controllerType);
    }


    //Grab callbacks

    public void OnGrabbableEnter(Grabbable script) //when a grabbable enters grabbing distance
    {
        if (!grabbables.Contains(script) && script != grabbedScript) //check if grabbable is already in the list or if it is already grabbed
        {
            //Debug.Log("Grabbable entered!");
            grabbables.Add(script); //add grabbable to list
        }
    }

    public void OnGrabbableExit(Grabbable script) //when a grabbable exits grabbing distance
    {
        //Debug.Log("Grabbable exited!");
        if(selectedScript != null && selectedScript == grabbables.Contains(script)) //deselect the grabbable if it was selected
        {
            grabbables.Remove(script); //remove grabbable from list
            Deselect();
            if (grabbables.Count > 0)
            {
                Select(grabbables[0]);
            }
        }
        
    }

    void Grab()
    {
        if (!grabbing)
        {
            grabbedScript = (Grabbable)selectedScript;
            Deselect();
            if (overUI)
            {
                overUI = false;
                lineRenderer.enabled = false;
            }
            grabbing = true;
            grabbedScript.GrabbedBy(this);
            controller.gripPressOut.AddListener(Drop);
            grabbables.Remove(grabbedScript);
        }
    }

    void Drop()
    {
        if (grabbing) //make sure we are grabbing
        {
            controller.gripPressOut.RemoveListener(Drop);
            grabbedScript.Drop();
            grabbedScript = null;
            grabbing = false;
        }
    }

    //UI callbacks

    void Use() //when a selectable element is pressed
    {
        if (overUI)
        {
            UIScript = selectedScript;
            pressedButton = UIScript;
            controller.aPress.RemoveListener(Use);
            selectedScript.OnSelectIn.Invoke(this);
            controller.aPressOut.AddListener(Release);
        }
    }

    void Release()
    {
        UIScript.OnSelectOut.Invoke(this);
    }
}
