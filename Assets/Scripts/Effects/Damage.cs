using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : Effect
{
    public Damage()
    {
        ID = 0;
        Name = "Damage";
    }
    public override void Prock(object target)
    {
        if (target is CharacterSheet)
        {
            (target as CharacterSheet).healthMod--;
        }
        else
        {
            throw new System.ArgumentException("Cant Damage something without Health");
        }
    }

    public override void Unprock(object target)
    {
        if (target is CharacterSheet)
        {
            (target as CharacterSheet).healthMod++;
        }
        else
        {
            throw new System.ArgumentException("Cant unDamage something without Health");
        }
    }
}
