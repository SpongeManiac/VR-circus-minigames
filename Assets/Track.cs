using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Track : MonoBehaviour
{
    public UnityEvent shipAdded;
    public UnityEvent shipDestroyed;
    public UnityEvent lastShip;

    [SerializeField]
    bool gameEnd = false;
    public ShipSpawner spawner;
    [SerializeField]
    GameObject shipPrefab;
    [SerializeField]
    List<GameObject> ships = new List<GameObject>();
    [SerializeField]
    int movementInterval = 10;

    public Transform start;
    public Transform end;

    public void Start()
    {

    }

    public void StartSpawns()
    {
        Debug.Log("Starting spawns");
        StartCoroutine(SpawnShips());
    }

    IEnumerator SpawnShips()
    {
        while(spawner.shipCount < spawner.maxShips && !spawner.gameEnd)
        {
            Debug.Log("Spawnship coroutine active");
            yield return new WaitForSeconds(4);
            MakeShip();
        }
    }


    Vector3 GetDirection(GameObject ship)
    {
        return (end.position - ship.transform.position).normalized;
    }

    void MakeShip()
    {
        if (spawner.shipCount < spawner.maxShips)
        {
            shipAdded.Invoke();
            var ship = Instantiate(shipPrefab, start);
            ships.Add(ship);
            var shipScript = ship.GetComponent<pirateControl>();
            shipScript.track = this;
            StartCoroutine(MoveShip(ship));
        }
        
    }

    public void RemoveShip(pirateControl script)
    {
        Debug.Log("Removing ship");
        shipDestroyed.Invoke();
        ships.Remove(script.gameObject);
        StopCoroutine(MoveShip(script.gameObject));
        if (!spawner.gameEnd)
        {
            MakeShip();
        }
        if (ships.Count <= 0)
        {
            lastShip.Invoke();
        }
    }

    IEnumerator MoveShip(GameObject ship)
    {
        while(ship != null)
        {
            yield return new WaitForEndOfFrame();
            if (ship == null)
            {
                yield break;
            }
            Debug.Log("Coroutine end of frame");
            var script = ship.GetComponent<pirateControl>();
            script.time += 1;

            if (script.time >= movementInterval)
            {
                script.time = 0;
                ship.transform.position += GetDirection(ship)/60;
            }
        }
    }
}
