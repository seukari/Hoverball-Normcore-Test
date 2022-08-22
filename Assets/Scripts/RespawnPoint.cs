using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{

    public new Collider collider;
    public int checkpointId;
    public Transform respawnAt;
    [Header("Colours")]
    public Color setColour;
    public Color unsetColour;
    [Header("Materials")]
    public Material checkpointReachedMat;
    public Material checkpointNotReachedMat;
    public Material checkpointReachedParticleMat;
    public Material checkpointNotReachedParticleMat;
    [Header("References")]
    public MeshRenderer renderer;
    public Light light;
    public ParticleSystem particles;
    public LineRenderer line;
    private ParticleSystemRenderer particleRenderer;
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip checkpointReachedClip;
    public CheckpointData data;



    // Start is called before the first frame update
    void Start()
    {
        RespawnPointManager.Instance.AddRespawnPoint?.Invoke(this);
        particleRenderer = particles.transform.GetComponent<ParticleSystemRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerReached()
    {
        RespawnPointManager.Instance.OnRespawnPointReached(this);

        renderer.material = checkpointReachedMat;
        light.color = setColour;
        line.startColor = setColour;
        line.endColor = setColour;
        var p = particles.main;
        p.startColor = setColour;
        particleRenderer.material = checkpointReachedParticleMat;
        if (data.Model != null)
        {
            data.RequestOwnership();

            data.Model.justReached = true;
            data.Model.justReached = false;

            data.ClearOwnership();
        }
        //PlayReachedClip();
    }

    public void UnsetReached()
    {
        renderer.material = checkpointNotReachedMat;
        light.color = unsetColour;
        line.startColor = unsetColour;
        line.endColor = unsetColour;
        var p = particles.main;
        p.startColor = unsetColour;
        particleRenderer.material = checkpointNotReachedParticleMat;

    }

    public void PlayReachedClip()
    {
        audioSource.PlayOneShot(checkpointReachedClip);
    }
}
