using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface UIValueElement<T>
{
    public void SetValue(T value);

    public T GetValue();

    public void SetName(string name);

    public UnityEvent<T> OnValueChanged();
}
