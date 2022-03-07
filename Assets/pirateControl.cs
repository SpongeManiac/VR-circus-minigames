using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class pirateControl : MonoBehaviour
{
    [SerializeField]
    List<GameObject> pirates;
    public Track track;
    public int time = 0;
    // Update is called once per frame
    void Start()
    {
        foreach(var pirate in pirates)
        {
            //start pirates
            var script = pirate.GetComponent<Pirate>();
            script.Reset();
        }
    }
    private void OnDestroy()
    {
        track.RemoveShip(this);
    }
}
