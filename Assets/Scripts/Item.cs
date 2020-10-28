using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    [SerializeField] private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    [SerializeField] private int value;

    public int Value
    {
        get { return value; }
        set { this.value = value; }
    }

    [SerializeField] private float tl;

    public float TL
    {
        get { return tl; }
        set { tl = value; }
    }

    [SerializeField] private float quantity;

    public float Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }

}
