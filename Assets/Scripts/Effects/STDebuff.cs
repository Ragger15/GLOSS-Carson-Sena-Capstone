using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STDebuff : Effect
{
    public STDebuff()
    {
        ID = 5;
        Name = "ST - 1";
    }
    public override void Prock(object target)
    {
        if (target is CharacterSheet)
        {
            (target as CharacterSheet).STMod--;
        }
        else
        {
            throw new System.ArgumentException("Cant give a ST debuff to something without ST");
        }
    }

    public override void Unprock(object target)
    {
        if (target is CharacterSheet)
        {
            (target as CharacterSheet).STMod++;
        }
        else
        {
            throw new System.ArgumentException("Cant take a ST debuff from something without ST");
        }
    }
}
