using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using System;

public class CheckpointData : RealtimeComponent<CheckpointDataModel>
{

    public RespawnPoint point;
    public CheckpointDataModel Model { get => model; }

    protected override void OnRealtimeModelReplaced(CheckpointDataModel previousModel, CheckpointDataModel currentModel)
    {
        //base.OnRealtimeModelReplaced(previousModel, currentModel);

        if (previousModel != null)
        {
            // Unregister from events
            previousModel.justReachedDidChange -= OnCheckpointJustReached;
        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
            {
                currentModel.justReached = false;
            }

            // Register for events so we'll know if the color changes later
            currentModel.justReachedDidChange += OnCheckpointJustReached;
        }

    }

    private void OnCheckpointJustReached(CheckpointDataModel model, bool value)
    {
        if (value)
        {
            point.PlayReachedClip();
        }
    }
}
