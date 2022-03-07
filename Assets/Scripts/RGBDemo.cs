using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Object = System.Object;
public class RGBDemo : MonoBehaviour //This component will color all things in it the same
{
    //place holder variables
    ColorInfo temp;
    MethodInfo setter;
    MethodInfo getter;
    Tuple<MethodInfo, MethodInfo> accessors;
    Tuple<Object[], Object[]> parameters;

    List<ColorInfo> colors = new List<ColorInfo>();// list of colors
    Dictionary<Component, ColorInfo> component2color = new Dictionary<Component, ColorInfo>();
    Color start = new Color(1, 0, 1, 1);
    Color color;

    public bool paused = true;
    bool started = false;

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

        public ColorInfo(Object target, Tuple<MethodInfo, MethodInfo> accessors, Tuple<Object[], Object[]> parameters)
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
            Object[] parameters = new Object[setVars.Count + 1];
            setVars.CopyTo(parameters);
            parameters[setVars.Count] = c;
            setter.Invoke(target, parameters);
        }
    }


    //rainbow settings
    public float cycleSpeed = 0.01f;
    bool up = false;
    int scale = 1;
    int channel = 0;

    // Update is called once per frame
    void Update()
    {
        if (!paused) 
        {
            //if process has not started, set start color
            if (!started)
            {
                color = start;
                started = true;
            }
            //Update each color
            RainbowShift();
        }
    }

    public void AddComponent(Component component) //add a component to be colorized
    {
        //get properties from target
        List<MemberInfo> members = new List<MemberInfo>(component.GetType().GetMembers());
        //remove all non-color properties
        members.RemoveAll((mi) => (mi.Name != "color"));
        MemberInfo member = null;
        //If there were no color properties, return
        if (members.Count == 0) { return; }
        //get the first color property
        member = members.Find((m) => (m.MemberType == MemberTypes.Property));
        //if there was none, return
        if (member == null) { return; }
        //get property information
        var info = (PropertyInfo)member;
        //get property accessors
        var access = info.GetAccessors();
        //set accessors
        setter = access[0];
        getter = access[1];
        accessors = new Tuple<MethodInfo, MethodInfo>(setter, getter);
        //set parameters
        parameters = new Tuple<Object[], Object[]>(new Object[] { }, new Object[] { });
        //create temporary ColorInfo
        temp = new ColorInfo(component, accessors, parameters);
        //add color to list if it isn't already in there
        if (!colors.Contains(temp))
        {
            component2color[component] = temp;
            colors.Add(temp);
        }
    }

    public void RemoveComponent(Component c) //remove component
    {
        Debug.Log("Removing component: "+c);
        if (component2color.TryGetValue(c, out var color))
        {
            //set original color
            component2color.Remove(c);
            colors.Remove(color);
            color.color = color.original;
        }
    }

    void RainbowShift()
    {
        
        float[] rgb = new float[] { color.r, color.g, color.b };
        if (up)
        {
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
            scale = -1;
            //Check if thresholds have been met
            if (rgb[channel] <= 0f)
            {
                rgb[channel] = 0f;  //make sure of no overflow
                SwapChannel();
            }
        }
        //change color
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
}
