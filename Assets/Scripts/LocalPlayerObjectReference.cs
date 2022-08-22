using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerObjectReference : MonoBehaviour
{
    private static LocalPlayerObjectReference instance;
    public static LocalPlayerObjectReference Instance { get { return instance; } }

    

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
    }


}
