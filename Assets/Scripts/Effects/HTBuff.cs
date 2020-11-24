using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTBuff : Effect
{
    public HTBuff()
    {
        ID = 4;
        Name = "HT + 1";
    }
    public override void Prock(object target)
    {
        if (target is CharacterSheet)
        {
            (target as CharacterSheet).HTMod++;
        }
        else
        {
            throw new System.ArgumentException("Cant give a HT buff to something without Health");
        }
    }

    public override void Unprock(object target)
    {
        if (target is CharacterSheet)
        {
            (target as CharacterSheet).HTMod--;
        }
        else
        {
            throw new System.ArgumentException("Cant take a HT buff from something without Health");
        }
    }
}
