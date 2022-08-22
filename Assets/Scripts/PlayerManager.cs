using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
#if NORMCORE

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _camera = default;
    [SerializeField] private GameObject _prefab;

    private Realtime _realtime;

    private void Awake()
    {
        Application.runInBackground = true;

        // Get the Realtime component on this game object
        _realtime = GetComponent<Realtime>();

        // Notify us when Realtime successfully connects to the room
        _realtime.didConnectToRoom += DidConnectToRoom;
    }

    private void DidConnectToRoom(Realtime realtime)
    {
        // Instantiate the Player for this client once we've successfully connected to the room
        var options = new Realtime.InstantiateOptions
        {
            ownedByClient = true,    // Make sure the RealtimeView on this prefab is owned by this client.
            preventOwnershipTakeover = true,    // Prevent other clients from calling RequestOwnership() on the root RealtimeView.
            useInstance = realtime // Use the instance of Realtime that fired the didConnectToRoom event.
        };
        GameObject playerGameObject = Realtime.Instantiate(_prefab.name, options);
        playerGameObject.transform.position = transform.position;
        // Get a reference to the player
        PlayerController player = playerGameObject.GetComponent<PlayerController>();

        // Get the constraint used to position the camera behind the player
        ParentConstraint cameraConstraint = _camera.GetComponent<ParentConstraint>();

        // Add the camera target so the camera follows it
        ConstraintSource constraintSource = new() { sourceTransform = player.transform, weight = 1.0f };
        int constraintIndex = cameraConstraint.AddSource(constraintSource);

        // Set the camera offset so it acts like a third-person camera.
        cameraConstraint.SetTranslationOffset(constraintIndex, new Vector3(0.0f, 1.0f, -4.0f));
        cameraConstraint.SetRotationOffset(constraintIndex, new Vector3(15.0f, 0.0f, 0.0f));

        DevSettings.Instance.SetPlayer(playerGameObject);

        SceneLoader.Instance.EndLoadingScreen();
    }
}


#endif