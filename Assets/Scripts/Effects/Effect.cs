using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class Effect
{
    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    private List<Condition> conditions = new List<Condition>();
    private bool isHidden = true;


    public bool IsHidden()
    {
        return isHidden;
    }

    public void SetHidden(bool hide)
    {
        isHidden = hide;
    }

    public abstract void Prock();

}
