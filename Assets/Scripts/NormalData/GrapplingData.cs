using Normal.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingData : RealtimeComponent<GrapplingDataModel>
{
    public LineRenderer line;
    public Transform player;

    public AudioSource audioSource;
    public AudioClip shootClip;

    public GrapplingDataModel Model { get => base.model; }

    private void Awake()
    {
        //if (model == null) model = new GrapplingDataModel();
        //OnRealtimeModelReplaced(new GrapplingDataModel(), new GrapplingDataModel());
    }

    protected override void OnRealtimeModelReplaced(GrapplingDataModel previousModel, GrapplingDataModel currentModel)
    {
        //base.OnRealtimeModelReplaced(previousModel, currentModel);

        if (previousModel != null)
        {
            // Unregister from events
            previousModel.hookPositionDidChange -= HookPositionDidChange;
            previousModel.hookExtendedDidChange -= HookExtendedChanged;
        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
            {
                currentModel.hookPosition = new Vector3();
                currentModel.hookExtended = false;
            }
                
            // Register for events so we'll know if the color changes later
            currentModel.hookPositionDidChange += HookPositionDidChange;
            currentModel.hookExtendedDidChange += HookExtendedChanged;
        }

        if (transform.root.GetComponent<RealtimeView>().isOwnedLocallySelf)
        {
            if (!isOwnedLocallySelf)
            {
                GetComponent<RealtimeView>().RequestOwnership();
            }
        }

        line.SetPosition(0, player.position);
        line.SetPosition(line.positionCount - 1, base.model.hookPosition);
        line.enabled = base.model.hookExtended;


    }

    private void HookPositionDidChange(GrapplingDataModel model, Vector3 value)
    {
        Debug.Log("HookPosChanged");
        line.SetPosition(0, player.position);
        line.SetPosition(line.positionCount - 1, value);
    }

    private void HookExtendedChanged(GrapplingDataModel model, bool value)
    {
        line.enabled = value;

        if (value)
        {
            audioSource.PlayOneShot(shootClip);
        }
    }

    private void Update()
    {
        if (model.hookExtended)
        {
            line.SetPosition(line.positionCount - 1, model.hookPosition);
            
        }
        line.enabled = base.model.hookExtended;

    }
}
