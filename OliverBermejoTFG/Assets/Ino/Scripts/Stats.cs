using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Stats{
    [SerializeField]
    private HealthScript bar;
    [SerializeField]
    private float maxValue;
    [SerializeField]
    private float currentValue;

    public float CurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {
            currentValue = value;
            bar.value = currentValue;
        }
    }

    public float MaxValue
    {
        get
        {
            return maxValue;
        }

        set
        {
            maxValue = value;
            bar.Maxvalue = maxValue;
        }
    }
    public void initialize() {

        this.MaxValue = maxValue;
        this.CurrentValue = currentValue;

    }
}
