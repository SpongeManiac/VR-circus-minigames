using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrigger : MonoBehaviour
{
    [SerializeField]
    protected Score score;

    private void Start()
    {
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("Ball scored");
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.GetComponent<Grabbable>().grabbable = false;
            //other.transform.parent = transform;
            other.gameObject.SetActive(false);
            score.addScore(20);
            Destroy(transform.parent.gameObject);
        }
    }
}
