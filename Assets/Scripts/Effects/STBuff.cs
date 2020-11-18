using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STBuff : Effect
{
    public STBuff()
    {
        ID = 1;
        Name = "ST + 1";
    }
    public override void Prock(object target)
    {
        if(target is CharacterSheet)
        {
            (target as CharacterSheet).STMod++;
        }
        else
        {
            throw new System.ArgumentException("Cant give a ST buff to something without Strength");
        }
    }

    public override void Unprock(object target)
    {
        if (target is CharacterSheet)
        {
            (target as CharacterSheet).STMod--;
        }
        else
        {
            throw new System.ArgumentException("Cant take a ST buff from something without Strength");
        }
    }
}
