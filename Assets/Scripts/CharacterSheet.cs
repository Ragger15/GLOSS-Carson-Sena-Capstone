using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterSheet
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

    [SerializeField] private float speed;

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public void SetSpeed(string spd)
    {
        Speed = float.Parse(spd);
    }
}
