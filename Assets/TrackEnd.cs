using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("other entered with tag of " + other.tag) ;
        if (other.CompareTag("Ship"))
        {
            Debug.Log("Ship entered, deleting.");
            var ship = other.gameObject.GetComponent<pirateControl>();
            Destroy(other.gameObject);
        }
    }
}
