using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RespawnPointManager : MonoBehaviour
{
    public UnityAction<RespawnPoint> AddRespawnPoint;
    public UnityAction<RespawnPoint> RespawnPointReached;
    public UnityAction<GameObject> SetLocalPlayer;


    public GameObject localPlayer;

    private List<RespawnPoint> respawnPoints = new List<RespawnPoint>();

    private static RespawnPointManager instance;
    public static RespawnPointManager Instance { get => instance; }

    private int currentCheckpoint;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        AddRespawnPoint += OnAddRespawnPoint;
        RespawnPointReached += OnRespawnPointReached;
        SetLocalPlayer += OnSetLocalPlayer;

    }

    private void OnSetLocalPlayer(GameObject obj)
    {
        localPlayer = obj;
    }

    public void OnAddRespawnPoint(RespawnPoint point)
    {
        respawnPoints.Add(point);
    }

    public void OnRespawnPointReached(RespawnPoint point)
    {
        currentCheckpoint = respawnPoints.IndexOf(point);
        //MoveObjectToCurrentSpawnPoint(localPlayer);

        for (int i = 0; i < respawnPoints.Count; i++)
        {
            if(currentCheckpoint != i)
            {
                respawnPoints[i].UnsetReached();
            }
        }

    }

    public void MoveObjectToCurrentSpawnPoint(GameObject obj)
    {
        obj.transform.position = respawnPoints[currentCheckpoint].respawnAt.position;
        Rigidbody objRb = obj.GetComponent<Rigidbody>();
        if (objRb != null)
        {
            objRb.velocity = new Vector3();
            objRb.angularVelocity = new Vector3();
        }

        PlayerGrapplingHook hook = obj.GetComponent<PlayerGrapplingHook>();
        if(hook != null)
        {
            hook.BreakHook();
        }
    }

}
