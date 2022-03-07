using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> balls = new List<GameObject>();
    Dictionary<GameObject, Vector3> defaultPos = new Dictionary<GameObject, Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(var ball in balls)
        {
            defaultPos[ball] = ball.transform.position;
        }
    }

    public void ResetBalls()
    {
        Debug.Log("Resetting balls");
        foreach(var ball in balls)
        {
            ball.transform.position = defaultPos[ball];
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().isKinematic = false;
            ball.GetComponent<Grabbable>().grabbable = true;
            ball.gameObject.SetActive(true);
        }
    }
    //reposition barrels

    //reset score
}
