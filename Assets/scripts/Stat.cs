using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class Stat {
    [SerializeField]
    private HealthBarScript bar;
    [SerializeField]
    private float maxVal;
    [SerializeField]
    private float currentVal;

    public float CurrentVal
    {
        get
        {
            return currentVal;
        }

        set
        {
            currentVal = value;
            bar.Value = currentVal;
            Debug.Log("OKAy");
        }
    }

    public float MaxVal
    {
        get
        {
            return maxVal;
        }

        set
        {
            maxVal = value;
            bar.MaxValue = maxVal;
        }
    }

    public void Initialize()
    {
        MaxVal = maxVal;
        CurrentVal = currentVal;
    }
}
