using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition
{
    private List<GameObject> conditionVaraibles = new List<GameObject>();
    private bool isHidden = true;


    public bool IsHidden()
    {
        return isHidden;
    }

    public void SetHidden(bool hide)
    {
        isHidden = hide;
    }

    public abstract void Check();
    public abstract void Notify();

}
