using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallExit : MonoBehaviour
{
    GameManager manager;
    // Start is called before the first frame update
    public int count = 0;
    List<GameObject> left = new List<GameObject>();

    private void Start()
    {
       manager = GameManager.instance;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball") && !left.Contains(other.gameObject))
        {
            //Debug.Log("Ball exited");
            left.Add(other.gameObject);
            count++;
        }
    }

    public void ResetCount()
    {
        count = 0;
        left.Clear();
    }

    private void Update()
    {
        //Debug.Log("Count: " + count);
        if (count == 4)
        {
            //wait 5 seconds, then end the game.
            manager.EndGame();
            count++;
        }
    }

    

}
