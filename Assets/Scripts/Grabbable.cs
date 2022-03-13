using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : Selectable
{
    [SerializeField]
    protected GameObject whole;
    protected Transform wholeParent;

    [SerializeField]
    protected Rigidbody rigidbody;

    [SerializeField]
    protected VRHand grabbedBy = null;
    [SerializeField]
    protected Vector3 targetLocation
    {
        get 
        {
            if (grabbedBy != null)
            {
                return grabbedBy.gripOffset.transform.position;
            }
            else
            {
                return Vector3.zero;
            }
        }
    }
    [SerializeField]
    protected Quaternion targetRotation
    {
        get
        {
            if (grabbedBy != null)
            {
                return grabbedBy.gripOffset.rotation;
                
            }
            else
            {
                return Quaternion.identity;
            }
        }
    }

    [SerializeField]
    protected Vector3 objectLocation
    {
        get
        {
            if (whole != null)
            {
                return whole.transform.position;
            }
            else
            {
                return Vector3.zero;
            }
        }

        set
        {
            whole.transform.position = value;
        }
    }
    [SerializeField]
    protected Quaternion objectRotation
    {
        get
        {
            if (whole != null)
            {
                return whole.transform.rotation;
            }
            else
            {
                return Quaternion.identity;
            }
            
        }
        set
        {
            whole.transform.rotation = value;
        }
    }
    [SerializeField]
    protected Vector3 vel = Vector3.zero;
    [SerializeField]
    protected Vector3 rVel = Vector3.zero;
    [SerializeField]
    public bool distanceGrabbable = false;
        [SerializeField]
        public float maxDistance = 5;
        [SerializeField]
        public float pullSpeed;
        [SerializeField]
        public bool pullFromOffset = true;
        
    
    public enum ParentMode { Hand, Object};
    [SerializeField]
    public ParentMode parentMode;
    [SerializeField]
    protected bool useOffset = false;
        [SerializeField]
        protected Transform offset;
        [SerializeField]
        protected bool ignorePosition = false;
        [SerializeField]
        protected bool ignoreRotation = false;

    protected enum ColorMode { Color, Rainbow};
    [SerializeField]
    protected ColorMode colorMode;
    [SerializeField]
    protected RGBDemo rgb;
    [SerializeField]
    protected ShaderColor color;
    [SerializeField]
    protected float outlineWidth = 0.02f;

    //grab states
    public bool grabbable { get { return _grabbable; } set { _grabbed = value; } }
    bool _grabbable = true;
    public bool grabbed { get { return _grabbed; } }
    bool _grabbed = false;


    protected override void Awake()
    {
        base.Awake();
        rgb = GetComponent<RGBDemo>();
        wholeParent = whole.transform.parent;
    }

    protected virtual void Update()
    {
    }

    protected override void onEnter(VRHand hand)
    {
        base.onEnter(hand);
        if (rgb != null)
        {
            rgb.AddComponent(color);
            rgb.paused = false;
            color.outlineWidth = outlineWidth;
        }
        
    }

    protected override void onExit(VRHand hand)
    {
        if (rgb != null)
        {
            rgb.paused = true;
            color.outlineWidth = 0;
            StartCoroutine("RemoveColor");
        }
        
    }

    protected virtual IEnumerator RemoveColor()
    {
        yield return new WaitForEndOfFrame();
        rgb.RemoveComponent(color);
    }

    

    public virtual void GrabbedBy(VRHand hand)
    {
        OnSelectIn.Invoke(hand);
        grabbedBy = hand;
        _grabbed = true;
        _grabbable = false;
        rigidbody.isKinematic = true;
        Debug.Log("Grabbed by controller: "+ hand.controller.controller);
        StartCoroutine("MoveToHand");
    }

    IEnumerator MoveToHand() //Create coroutine to lerp item into hand (rotation and position)
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            Debug.Log("MoveToHand routine started");
            if (grabbedBy != null)
            {
                var distance = (targetLocation - objectLocation).magnitude;
                Debug.Log("Object Distance From Hand: "+distance);
                Debug.Log("Hand Location: " + targetLocation);
                Debug.Log("Object Location: " + objectLocation);
                if (Mathf.Abs(distance) <= 0.3)
                {
                    //move point to position, then parent it
                    whole.transform.position = grabbedBy.gripOffset.position;
                    whole.transform.rotation = grabbedBy.gripOffset.rotation;
                    whole.transform.parent = grabbedBy.gripOffset.transform;
                    if (offset != null)
                    {
                        whole.transform.localPosition -= offset.position;
                        whole.transform.rotation = Quaternion.Euler(whole.transform.rotation.eulerAngles + offset.localRotation.eulerAngles);
                    }
                    StopCoroutine("MoveToHand");
                    break;
                }
                var moveDirection = (targetLocation - objectLocation).normalized;
                var speed = 0.33f;
                objectLocation = Vector3.Lerp(objectLocation, targetLocation, speed); //+ offset.localPosition;
                objectRotation = Quaternion.Lerp(objectRotation, targetRotation, speed);

            }
            else
            {
                StopCoroutine("MoveToHand");
            }
        }
    }

    public virtual void Drop()
    {
        whole.transform.parent = wholeParent;
        rigidbody.isKinematic = false;
        rigidbody.velocity = grabbedBy.velocity*1.2f;
        grabbedBy = null;
        _grabbed = false;
        _grabbable = true;
    }
}
