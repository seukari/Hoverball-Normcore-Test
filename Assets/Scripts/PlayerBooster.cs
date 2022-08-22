using Normal.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBooster : MonoBehaviour
{

    public InputActionReference boostAction;

    public float boostFuelMax = 1;
    public float boostFuel = 1;
    public float boostPower = 15;
    public float boostFuelChargeRate = 1;

    private RealtimeView view;

    public BoostData boostData;
    private Rigidbody rb;
    public Transform cameraBoom;
    private UnityEngine.UI.Image boostChargeBar;

    private bool boosting = false;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<RealtimeView>();

        if (!view.isOwnedLocallySelf) return;

        rb = GetComponent<Rigidbody>();

        boostAction.action.Enable();

        boostAction.action.started += BoostStarted;
        boostAction.action.canceled += BoostCancelled;

        if (cameraBoom == null)
        {
            cameraBoom = GameObject.Find("CameraBoom").transform;
        }

        boostChargeBar = GameObject.Find("BoostChargeBar").GetComponent<UnityEngine.UI.Image>();

    }

    private void BoostCancelled(InputAction.CallbackContext obj)
    {
        boosting = false;

    }

    private void BoostStarted(InputAction.CallbackContext obj)
    {
        boosting = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (view.isOwnedLocallySelf)
        {
            if (boostData.Model != null)
            {
                if (boosting)
                {
                    if (boostFuel > 0)
                    {
                        boostFuel -= Time.deltaTime;
                        rb.AddForce(cameraBoom.forward * boostPower, ForceMode.Force);
                        boostChargeBar.fillAmount = boostFuel / boostFuelMax;
                        boostData.Model.boosting = true;
                    }
                    else
                    {
                        boostData.Model.boosting = false;
                    }

                }
                else
                {
                    if (boostFuel < boostFuelMax)
                    {
                        boostFuel += Time.deltaTime * boostFuelChargeRate;
                        boostFuel = Mathf.Clamp(boostFuel, 0, boostFuelMax);
                        boostChargeBar.fillAmount = boostFuel / boostFuelMax;
                    }
                    boostData.Model.boosting = false;
                }
            }
        }
        
    }
}
