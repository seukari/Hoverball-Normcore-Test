using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DevSettings : MonoBehaviour
{
    private static DevSettings instance;
    public static DevSettings Instance { get => instance; }


    public InputActionReference devKey;
    public GameObject devPanel;
    public GameObject endGameText;
    public PlayerBooster booster;
    public PlayerController controller;
    public PlayerGrapplingHook grapple;
    [Header("UI")]
    public UISliderElement moveSpeedSlider;
    public UISliderElement hookRetractForceSlider;
    public UISliderElement hookRangeSlider;
    public UIToggleElement hookRetractToggle;
    public UISliderElement boostFuelSlider;
    public UISliderElement boostRechargeSlider;
    public UISliderElement boostPowerSlider;


    private bool devMode = true;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        devKey.action.Enable();

        devKey.action.performed += OnToggleDevMode;
        ToggleDevMode();
    }

    private void ToggleDevMode()
    {
        devMode = !devMode;
        devPanel.SetActive(devMode);
        Cursor.visible = devMode;
        
        if (devMode)
        {
            Cursor.lockState = CursorLockMode.None;
            if (endGameText.activeSelf)
            {
                endGameText.SetActive(false);
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;

        }
    }

    private void OnDisable()
    {
        devKey.action.performed -= OnToggleDevMode;

        moveSpeedSlider.OnValueChanged().RemoveListener(OnMoveSpeedChanged);

    }

    private void OnToggleDevMode(InputAction.CallbackContext obj)
    {
        ToggleDevMode();
    }

    public void SetPlayer(GameObject player)
    {
        booster = player.GetComponent<PlayerBooster>();
        controller = player.GetComponent<PlayerController>();
        grapple = player.GetComponent<PlayerGrapplingHook>();


        moveSpeedSlider.SetMinMax(0, 1000);
        moveSpeedSlider.SetValue(controller.moveSpeed);
        moveSpeedSlider.OnValueChanged().AddListener(OnMoveSpeedChanged);

        hookRetractForceSlider.SetMinMax(0, 20);
        hookRetractForceSlider.SetValue(grapple.hookRetractForce);
        hookRetractForceSlider.OnValueChanged().AddListener(OnHookRetractForceChanged);

        hookRangeSlider.SetMinMax(10, 100);
        hookRangeSlider.SetValue(grapple.maxRange);
        hookRangeSlider.OnValueChanged().AddListener(OnHookRangeChanged);

        hookRetractToggle.SetValue(grapple.retractToDistance);
        hookRetractToggle.OnValueChanged().AddListener(OnHookRetractChanged);

        boostPowerSlider.SetMinMax(0, 10);
        boostPowerSlider.SetValue(booster.boostPower);
        boostPowerSlider.OnValueChanged().AddListener(OnBoostPowerChanged);

        boostFuelSlider.SetMinMax(0, 20);
        boostFuelSlider.SetValue(booster.boostFuelMax);
        boostFuelSlider.OnValueChanged().AddListener(OnBoostFuelChanged);

        boostRechargeSlider.SetMinMax(0, 20);
        boostRechargeSlider.SetValue(booster.boostFuelChargeRate);
        boostRechargeSlider.OnValueChanged().AddListener(OnBoostRechargeChanged);
    }

    private void OnBoostRechargeChanged(float value)
    {
        booster.boostFuelChargeRate = value;
    }

    private void OnBoostFuelChanged(float value)
    {
        booster.boostFuelMax = value;
    }

    private void OnBoostPowerChanged(float value)
    {
        booster.boostPower = value;
    }

    private void OnHookRetractChanged(bool value)
    {
        grapple.retractToDistance = value;
    }

    private void OnHookRangeChanged(float value)
    {
        grapple.maxRange = value;
    }

    private void OnHookRetractForceChanged(float value)
    {
        grapple.hookRetractForce = value;
    }

    private void OnMoveSpeedChanged(float value)
    {
        controller.moveSpeed = value;
    }

    public void RefreshValues()
    {

    }
}
