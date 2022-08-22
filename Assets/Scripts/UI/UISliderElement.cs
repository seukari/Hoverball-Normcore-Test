using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISliderElement : MonoBehaviour, UIValueElement<float>
{
    
    public float value = 0;
    public Vector2 minMax = new Vector2();

    public Slider valueSlider;
    public TMPro.TMP_InputField valueInput;
    public TMPro.TMP_Text nameLabel;

    private UnityEvent<float> onValueChanged = new UnityEvent<float>();

    private void Awake()
    {
        SetMinMax(minMax.x, minMax.y);
        SetValue(value);
    }

    public float GetValue()
    {
        return value;
    }

    public void SetValue(float value)
    {
        this.value = value;
        valueSlider.value = value;
        valueInput.text = value.ToString("F2");
        onValueChanged?.Invoke(value);
    }

    public void SetName(string name)
    {
        nameLabel.name = name;
    }

    public void SetMinMax(float min, float max)
    {
        minMax = new Vector2(min, max);
        valueSlider.minValue = min;
        valueSlider.maxValue = max;
    }

    public void SetValue(string value)
    {
        float stringVal = float.Parse(value);

        SetValue(stringVal);
    }

    public UnityEvent<float> OnValueChanged()
    {
        return onValueChanged;
    }
}
