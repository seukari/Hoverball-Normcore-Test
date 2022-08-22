using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainStartingLocalPosition : MonoBehaviour
{
    public Vector3 startingPos;

    private void Start()
    {
        startingPos = transform.localPosition;
    }

    private void Update()
    {
        transform.position = transform.parent.position + startingPos;
    }
}
