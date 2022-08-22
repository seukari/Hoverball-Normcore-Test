using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIToggleElement : MonoBehaviour, UIValueElement<bool>
{
    
    public bool value = false;

    public Toggle valueToggle;
    public TMPro.TMP_Text nameLabel;

    private UnityEvent<bool> onValueChanged = new UnityEvent<bool>();

    private void Awake()
    {
        SetValue(value);
    }

    public bool GetValue()
    {
        return value;
    }

    public void SetValue(bool value)
    {
        this.value = value;
        valueToggle.isOn = value;
        onValueChanged?.Invoke(value);
    }

    public void SetName(string name)
    {
        nameLabel.name = name;
    }

    public void SetValue(string value)
    {
        bool stringVal = bool.Parse(value);

        SetValue(stringVal);
    }

    public UnityEvent<bool> OnValueChanged()
    {
        return onValueChanged;
    }
}
