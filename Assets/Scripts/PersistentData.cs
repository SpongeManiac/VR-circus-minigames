using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PersistentData
{
    public object data;
    public System.Type type { get { return data.GetType(); } }
    public PersistentData(object data)
    {
        this.data = data;
    }
}
