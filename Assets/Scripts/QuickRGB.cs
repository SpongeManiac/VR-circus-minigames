using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;
using Object = System.Object;

public class QuickRGB : MonoBehaviour
{
    public List<Component> targets;
    List<ColorInfo> colors = new List<ColorInfo>();
    Color start = new Color(1, 0, 1, 1);
    Color color;
    public bool pause = false;
    public bool paused = false;
    public bool wasUnpaused = false;


    //data type to hold color accessors
    public class ColorInfo
    {
        public Object target;
        MethodInfo setter;
        List<Object> setVars = new List<Object>();
        MethodInfo getter;
        List<Object> getVars = new List<Object>();
        public Color original;
        public Renderer renderer;
        public Color color { get { return GetColor(); } set { SetColor(value); } }

        public ColorInfo(Object target, Tuple<MethodInfo,MethodInfo> accessors, Tuple<Object[], Object[]> parameters)
        {
            this.target = target;
            setter = accessors.Item1;
            setVars = new List<Object>(parameters.Item1);
            getter = accessors.Item2;
            getVars = new List<Object>(parameters.Item2);
            original = color;
        }

        Color GetColor()
        {
            return (Color)getter.Invoke(target, getVars.ToArray());
        }

        void SetColor(Color c)
        {
            Object[] parameters = new Object[setVars.Count+1];
            setVars.CopyTo(parameters);
            parameters[setVars.Count] = c;
            setter.Invoke(target, parameters);
        }
    }


    //rainbow settings
    public bool started = false;
    private float cycleSpeed = 0.01f;
    public bool up = false;
    public int scale = 1;
    public int channel = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Component count: " + targets.Count);
        foreach (var component in targets)
        {
            Debug.Log("Current Component: "+component.GetType());
            ColorInfo temp;
            MethodInfo setter;
            MethodInfo getter;
            Tuple<MethodInfo, MethodInfo> accessors;
            Tuple<Object[], Object[]> parameters;

            if (component.TryGetComponent<Renderer>(out var renderer) && renderer == component)
            {
                //setup renderer for rainbow highlight
                setter = renderer.material.GetType().GetMethod("SetColor", new Type[] { typeof(string), typeof(Color) });
                getter = renderer.material.GetType().GetMethod("GetColor", new Type[] { typeof(string) });
                accessors = new Tuple<MethodInfo, MethodInfo>(setter, getter);
                parameters = new Tuple<Object[], Object[]>(new Object[] { "_OutlineColor" }, new Object[] { "_OutlineColor" });
                temp = new ColorInfo(renderer.material, accessors, parameters);
                temp.renderer = renderer;
                colors.Add(temp);
                continue;
            }
            //get properties from target
            List<MemberInfo> members = new List<MemberInfo>(component.GetType().GetMembers());
            //remove all non-color properties
            members.RemoveAll((mi) => (mi.Name != "color"));
            MemberInfo member = null;
            //If there were no color properties, continue
            if (members.Count == 0){continue;}
            member = members.Find((m) => (m.MemberType==MemberTypes.Property));
            if (member == null){continue;}
            var info = (PropertyInfo)member;
            var access = info.GetAccessors();
            setter = access[0];
            getter = access[1];
            accessors = new Tuple<MethodInfo, MethodInfo>( setter, getter);
            parameters = new Tuple<Object[], Object[]>(new Object[] { }, new Object[] { });
            temp = new ColorInfo(component, accessors, parameters);
            colors.Add(temp);
        }
        Debug.Log("Total colors: "+colors.Count);
        foreach (var c in colors)
        {
            Debug.Log("Color: "+c);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //check if script is enabled
        if (!paused)
        {

            //if the process has just begun, initialize the colors
            //and reset RGB shift logic
            if (!started)
            {
                color = start;
                foreach (var color in colors)
                {
                    color.color = this.color;
                }
                up = false;
                started = !started;
            }
            //actually shift the RGB
            RainbowShift();
        }
        else 
        {
            if (wasUnpaused)
            {
                ResetColors();
            }
        }
        wasUnpaused = !paused;
    }

    void RainbowShift() //shifts one channel at a time, switching between increasing and decreasing on each channel.
    {
        float[] rgb = new float[] { color.r, color.g, color.b };
        
        if (up)
        {
            //scale is positive, causing channel to increase
            scale = 1;
            //Check if thresholds have been met
            if (rgb[channel] >= 1f)
            {
                rgb[channel] = 1f; //make sure of no overflow
                SwapChannel();
            }
        }
        else
        {
            //scale is negative, causing channel to decrease
            scale = -1;
            //Check if thresholds have been met
            if (rgb[channel] <= 0f)
            {
                rgb[channel] = 0f;  //make sure of no overflow
                SwapChannel();
            }
        }
        //set color
        rgb[channel] += cycleSpeed * scale;

        //update current color
        color = new Color(rgb[0], rgb[1], rgb[2], 1f);
        //update each color
        foreach (ColorInfo color in colors)
        {
            color.color = this.color;
        }
    }

    void SwapChannel()
    {
        //switch to next channel without going over 2 (loop back to 0)
        channel = (channel + 1) % 3;
        //swap current direction for next iteration
        up = !up;
        //set scale to be opposite
        scale *= -1;
    }

    void ResetColors()
    {
        //reset colors to original
        foreach (ColorInfo color in colors)
        {
            color.color = color.original;
        }
    }
}
