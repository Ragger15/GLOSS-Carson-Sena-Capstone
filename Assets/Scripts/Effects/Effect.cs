using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class Effect
{
    public int ID { get; set; }
    public string Name { get; set; }
    public abstract void Prock(object target);
    public abstract void Unprock(object target);

}
