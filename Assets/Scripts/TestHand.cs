using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class TestHand : MonoBehaviour
{
    /*[SerializeField]
    protected VRController controller;
    [SerializeField]
    protected TestHand otherHand;
    //determines which hand is selecting items/buttons
    protected bool selector = false;
    [SerializeField]
    protected float swapCooldown = 5;
    [SerializeField]
    protected float triggerSpeed = 60;
    protected float triggerTime = 0;
    protected bool addTrigger = false;
    protected bool checkingSwap = false;
    [SerializeField]
    protected LineRenderer lineRenderer;
    protected bool pointing { 
        get { return OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controllerType) > 0 && !OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, controllerType); }
    }

    //hand settings
    [SerializeField]
    protected OVRInput.Controller controllerType;
    [SerializeField]
    protected Collider gripDetector;
    [SerializeField]
    public Transform gripOffset;
    [SerializeField]
    protected GameObject finger;
    [SerializeField]
    protected GameObject fingerTip;
    [SerializeField]
    protected LayerMask interactionMask;


    //interaction
    [SerializeField]
    protected bool enableGrab;
    protected bool intereacting = false;
    protected bool wasInteracting = false;

    //data
    public List<GameObject> grabbable { get { return _grabbable; } protected set { _grabbable = value; } }
    List<GameObject> _grabbable = new List<GameObject>();
    [SerializeField]
    protected bool grabbing = false;
    [SerializeField]
    protected GameObject grabbedObject = null;

    private bool selected = false;
    private GameObject selectedObject;
    private GameObject lastSelected = null;

    private GameObject pressInitator = null;

    private void Awake()
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        //Make sure linerenderer is invisible
        lineRenderer.enabled = false;
        //set controller to check when it becomes selector
        SelectorTrigger();
    }

    // Update is called once per frame
    void Update()
    {
        //check if selector
        if (selector)
        {
            //Make line indicating user intenetion
            MakeLine();
            if(wasInteracting && !intereacting)
            {
                DisableLine();
            }
            if(selected == true)
            {
                //do something if something is selected
            }
            wasInteracting = intereacting;
        }
        if (!grabbing && grabbable.Count > 0 && selectedObject != grabbable[0])
        {
            Debug.Log("Highlighting grabbable.");
            Highlight(grabbable[0]);
        }
    }

    void SelectorTrigger()
    {
        controller.triggerTap.AddListener(SetSelector);
    }

    void SetSelector()
    {
        //make sure trigger is gone now that we are selector
        controller.triggerTap.RemoveListener(SetSelector);
        //deselect selected item in the other hand
        otherHand.Clear(otherHand.selectedObject);
        //make sure nothing is selected in current hand and that the line is disabled
        DisableLine();
        //assures other hand is not the selector, then makes this hand the selector
        otherHand.selector = false;
        selector = true;
        //finally, set other hand to wait until it's selector is set
        otherHand.SelectorTrigger();
        TimedVibrate(0.1f, 1, 0.1f);
    }

    void MakeLine()
    {
        
        //get finger position and direction towards finger tip
        Vector3 p1 = finger.transform.position;
        Vector3 p2 = -(p1 - fingerTip.transform.position).normalized;
        //create a ray from p1 in the direction of p2
        Ray ray = new Ray(p1, p2);
        //Set the collision filter and ray distance
        if (Physics.Raycast(ray, out RaycastHit hit, 10f, interactionMask))
        {
            //Debug.Log("Line hit");
            //line rendering
            lineRenderer.enabled = true;
            intereacting = true;
            lineRenderer.startWidth = 0.01f;
            lineRenderer.endWidth = 0.01f;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, p1);
            lineRenderer.SetPosition(1, hit.point);

            //object selection
            var obj = hit.collider.gameObject;
            //check if current object pointed at is different if we have something selected alread and if so deselect current object.
            if (obj.TryGetComponent<Selectable>(out var selectable))
            {
                //if the selectable is a grabbable
                if (obj.TryGetComponent<Grabbable>(out var grab) && !grabbing)
                {
                    Debug.Log("Hit grabbable");
                    if(grab.distanceGrabbable && hit.distance <= grab.maxDistance)
                    {
                        Debug.Log("Highlighting distance grabbable");
                        //select the grabbable
                        Highlight(obj);
                    }
                }//otherwise, treat it as normal
                else
                {
                    if (!selected)
                    {
                        Highlight(obj);
                    }
                    else
                    {
                        //something was already selected
                        //check if they are the same
                        if (selectedObject == obj) { return; }
                        else
                        {
                            //if the current selected is a button, release it & clear it
                            DisableSelected();
                            Highlight(obj);
                        }
                    }
                }
                
            }
            else //something was previously selected, release out if it was a button
            {
                if(selected)
                {
                    DisableSelected();
                }
            }
        }
        else
        {
            //disable line
            DisableLine();
        }
    }

    void DisableLine()
    {
        Clear(selectedObject);
        lineRenderer.enabled = false;
        intereacting = false;
    }
    

    //events
    public void CanGrabEnter(GameObject grabbable)
    {
        //TimedVibrate(0.1f, 0.6f, 0.3f);
        if (!this.grabbable.Contains(grabbable))
        {
            this.grabbable.Add(grabbable);
            //controller.gripPress.AddListener(grabb);
            Debug.Log(string.Format("{0} is now grabbable", grabbable.gameObject));
        }
    }
    public void CanGrabExit(GameObject grabbable)
    {
        if (this.grabbable.Contains(grabbable))
        {
            this.grabbable.Remove(grabbable);
            controller.gripPress.RemoveListener(PressSelected);
            Debug.Log(string.Format("{0} is now ungrabbable", grabbable.gameObject));
            if (grabbable == selectedObject)
            {
                Clear(grabbable);
            }
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

    void Highlight(GameObject obj)
    {
        
        lastSelected = selectedObject;
        selectedObject = obj;
        if (!selected)
        {
            selected = true;
        }

        if (obj.TryGetComponent<Grabbable>(out var grabScript))
        {
            grabScript.OnEnter.Invoke();
            controller.gripPress.AddListener(PressSelected);
        }
        if (obj.TryGetComponent<VRButton>(out var btn))
        {
            controller.aPress.AddListener(PressSelected);
            controller.aRelease.AddListener(ReleaseSelected);
            btn.OnEnter.Invoke();
        }
        
    }

    void DisableSelected()
    {
        if (selectedObject.TryGetComponent<VRButton>(out var btn))
        {
            btn.OnReleaseOut.Invoke();
        }
        Clear(selectedObject);
    }

    void Clear(GameObject obj)
    {
        if (obj != null)
        {
            //turn off color outline
            var selectable = obj.GetComponent<Selectable>();
            selectable.OnExit.Invoke();
        }
        selectedObject = null;
        selected = false;
    }

    void PressSelected()
    {
        if (selectedObject.TryGetComponent<VRButton>(out var btn))
        {
            //Debug.Log("Invoking press");
            pressInitator = selectedObject;
            btn.OnSelect.Invoke();
        }
        if (selectedObject.TryGetComponent<Grabbable>(out var grab))
        {
            Debug.Log("Grabbed " + grab.gameObject + "!");
            grabbing = true;
            grabbedObject = selectedObject;
            controller.gripRelease.AddListener(ReleaseSelected);
            //grab.GrabbedBy(this);
        }
    }

    void GrabSelected()
    {

    }

    void DropGrabbed()
    {

    }

    void ReleaseSelected()
    {
        //Debug.Log("Invoking release");
        if (selected && selectedObject.TryGetComponent<VRButton>(out var btn) && pressInitator == selectedObject)
        {
            btn.OnReleaseIn.Invoke();
            return; //quick fix to prevent buttons from releasing what is held
        }
        if (grabbing)
        {
            Debug.Log("Dropped "+grabbedObject+"!");
            grabbedObject.GetComponent<Grabbable>().Drop();
            grabbedObject = null;
            grabbing = false;
        }
    }*/
}