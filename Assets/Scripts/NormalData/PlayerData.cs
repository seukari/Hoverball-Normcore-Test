using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using System;

public class PlayerData : RealtimeComponent<PlayerDataModel>
{

    public TMPro.TMP_Text nameLabel;
    public PlayerDataCarrier playerData;
    public PlayerColourSetter colourSetter;

    private void Start()
    {
        if (transform.root.GetComponent<RealtimeView>().isOwnedLocallySelf)
        {
            if (!isOwnedLocallySelf)
            {
                GetComponent<RealtimeView>().RequestOwnership();

            }
            playerData = FindObjectOfType<PlayerDataCarrier>();

            model.name = playerData.name;
            model.colour = "#" + ColorUtility.ToHtmlStringRGBA(playerData.color);
            Color color;
            if (ColorUtility.TryParseHtmlString(model.colour, out color))
            {
                colourSetter.SetColour(color);
            }

        }
        else
        {
            nameLabel.text = model.name;

            Color color;
            if (ColorUtility.TryParseHtmlString(model.colour, out color))
            {
                colourSetter.SetColour(color);
            }
        }

        
    }

    protected override void OnRealtimeModelReplaced(PlayerDataModel previousModel, PlayerDataModel currentModel)
    {
        //base.OnRealtimeModelReplaced(previousModel, currentModel);

        if (previousModel != null)
        {
            // Unregister from events
            previousModel.nameDidChange -= OnNameDidChange;
            previousModel.colourDidChange -= OnColourDidChange;
        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
            {
                currentModel.name = "Unnamed Player";
                currentModel.colour = "#" + ColorUtility.ToHtmlStringRGBA(Color.cyan);
            }

            // Register for events so we'll know if the color changes later
            currentModel.nameDidChange += OnNameDidChange;
            currentModel.colourDidChange += OnColourDidChange;
        }

        if (transform.root.GetComponent<RealtimeView>().isOwnedLocallySelf)
        {
            if (!isOwnedLocallySelf)
            {
                GetComponent<RealtimeView>().RequestOwnership();
            }
        }
    }

    private void OnColourDidChange(PlayerDataModel model, string value)
    {
        Color color;
        if(ColorUtility.TryParseHtmlString(value,out color))
        {
            colourSetter.SetColour(color);
        }
    }

    private void OnNameDidChange(PlayerDataModel model, string value)
    {
        if (value == "")
            value = "Unnamed Player";
        nameLabel.text = value;
        
    }
}
