using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectsOnCollide : MonoBehaviour
{

    public List<GameObject> objs = new List<GameObject>();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<RealtimeView>().isOwnedLocallySelf)
            {
                EnableObjs();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<RealtimeView>().isOwnedLocallySelf)
            {
                EnableObjs();
            }
        }
    }

    private void EnableObjs()
    {
        for (int i = 0; i < objs.Count; i++)
        {
            objs[i].SetActive(true);
        }
    }
}
