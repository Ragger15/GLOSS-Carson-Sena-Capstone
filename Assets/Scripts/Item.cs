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

    public void SetName(string nme)
    {
        Name = nme;
    }

    [SerializeField] private int value;

    public int Value
    {
        get { return value; }
        set { this.value = value; }
    }

    public void SetValue(string vlue)
    {
        Value = int.Parse(vlue);
    }

    [SerializeField] private float tl;

    public float TL
    {
        get { return tl; }
        set { tl = value; }
    }

    public void SetTL(string TLL)
    {
        TL = float.Parse(TLL);
    }

    [SerializeField] private float quantity;

    public float Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }

    public void SetQuantity(string qntituy)
    {
        Quantity = float.Parse(qntituy);
    }

}
