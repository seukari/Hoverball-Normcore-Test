using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPointDetectPlayer : MonoBehaviour
{
    public RespawnPoint point;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (other.GetComponent<RealtimeView>().isOwnedLocallySelf)
            {
                point.PlayerReached();
            }
        }
    }
}
