using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTDebuff : Effect
{
    public HTDebuff()
    {
        ID = 8;
        Name = "HT - 1";
    }
    public override void Prock(object target)
    {
        if (target is CharacterSheet)
        {
            (target as CharacterSheet).HTMod--;
        }
        else
        {
            throw new System.ArgumentException("Cant give a HT debuff to something without HT");
        }
    }

    public override void Unprock(object target)
    {
        if (target is CharacterSheet)
        {
            (target as CharacterSheet).HTMod++;
        }
        else
        {
            throw new System.ArgumentException("Cant take a HT debuff from something without HT");
        }
    }
}
