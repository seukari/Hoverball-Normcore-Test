using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExitOnKey : MonoBehaviour
{
    public InputActionReference key;

    // Start is called before the first frame update
    void Start()
    {
        key.action.Enable();
        key.action.performed += KeyPerformed;
    }

    private void KeyPerformed(InputAction.CallbackContext obj)
    {
        Application.Quit();
    }
}
