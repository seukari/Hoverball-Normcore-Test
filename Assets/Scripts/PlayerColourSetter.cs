using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColourSetter : MonoBehaviour
{

    public List<MeshRenderer> renderers = new List<MeshRenderer>();
    private Material mat;
    public LineRenderer grapple;
    public ParticleSystemRenderer boostParticles;
    // Start is called before the first frame update
    public Color col;
    void Start()
    {
        mat = new Material(renderers[0].material);
        for (int i = 0; i < renderers.Count; i++)
        {
            renderers[i].material = mat;
        }

        grapple.material = new Material(grapple.material);
        boostParticles.material = new Material(boostParticles.material);

    }

    public void SetColour(Color color)
    {
        col = color;

        grapple.material.color = col;
        grapple.material.EnableKeyword("_EMISSION");
        grapple.material.SetColor("_EmissionColor", color);
        grapple.startColor = color;
        grapple.endColor = color;

        boostParticles.material.color = color;
        boostParticles.material.EnableKeyword("_EMISSION");
        boostParticles.material.SetColor("_EmissionColor", color);

        mat.color = color;
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", color);
        DynamicGI.UpdateEnvironment();
    }
}
