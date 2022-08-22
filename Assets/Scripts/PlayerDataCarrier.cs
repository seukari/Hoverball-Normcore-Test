using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataCarrier : MonoBehaviour
{
    public new string name;
    public Color color;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetName(string value)
    {
        name = value;
    }

    public void SetColour(Color value)
    {
        color = value;
    }
}
