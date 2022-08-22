using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnOnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<RealtimeView>().isOwnedLocallySelf)
            {
                RespawnPointManager.Instance.MoveObjectToCurrentSpawnPoint(collision.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<RealtimeView>().isOwnedLocallySelf)
            {
                RespawnPointManager.Instance.MoveObjectToCurrentSpawnPoint(other.gameObject);
            }
        }
    }
}
