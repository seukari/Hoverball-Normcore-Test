using Normal.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostData : RealtimeComponent<BoostDataModel>
{
    public ParticleSystem boostingParticles;

    public BoostDataModel Model { get => model; }

    

    protected override void OnRealtimeModelReplaced(BoostDataModel previousModel, BoostDataModel currentModel)
    {
        //base.OnRealtimeModelReplaced(previousModel, currentModel);

        if (previousModel != null)
        {
            // Unregister from events
            previousModel.boostingDidChange -= BoostingDidChange;
        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
            {
                currentModel.boosting = false;
            }

            // Register for events so we'll know if the color changes later
            currentModel.boostingDidChange += BoostingDidChange;
        }

        if (transform.root.GetComponent<RealtimeView>().isOwnedLocallySelf)
        {
            if (!isOwnedLocallySelf)
            {
                GetComponent<RealtimeView>().RequestOwnership();
            }
        }

        
    }

    private void BoostingDidChange(BoostDataModel model, bool value)
    {
        var em = boostingParticles.emission;
        em.enabled = value;
    }
}
