using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBasket : MonoBehaviour
{
    [SerializeField]
    protected int numBaskets;
    [SerializeField]
    List<GameObject> baskets = new List<GameObject>();
    [SerializeField]
    protected GameObject basket;
    List<Vector3> positions = new List<Vector3>();

    [SerializeField]
    protected Collider spawnZone;

    Vector3 randomPos
    {
        get
        {
            return new Vector3(Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x),
                Random.Range(spawnZone.bounds.min.y, spawnZone.bounds.max.y),
                Random.Range(spawnZone.bounds.min.z, spawnZone.bounds.max.z));
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Spawning baskets");
        spawnZone.isTrigger = true;
        SpawnBaskets();
    }

    void SpawnBaskets()
    {
        int i = numBaskets;
        while(i > 0)
        {
            var temp = Instantiate(basket);
            bool gettingPos = true;
            bool valid = true;
            Vector3 newPos = randomPos;
            while (gettingPos)
            {
                valid = true;
                foreach (var op in baskets)
                {
                    var distance = Mathf.Abs(Vector3.Distance(newPos, op.transform.position));
                    //Debug.Log(distance);
                    if (distance < 0.5)
                    {
                        valid = false;
                        break;
                    }
                }
                if (!valid)
                {
                    newPos = randomPos;
                }
                else
                {
                    gettingPos = false;
                }
            }
            temp.transform.position = newPos;
            baskets.Add(temp);
            i--;
        }
    }

    public void RepositionBaskets()
    {
        while (baskets.Count > 0)
        {
            var tmp = baskets[0];
            baskets.Remove(tmp);
            Destroy(tmp);
        }
        SpawnBaskets();
    }

}
