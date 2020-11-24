using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DXDebuff : Effect
{
    public DXDebuff()
    {
        ID = 6;
        Name = "DX - 1";
    }
    public override void Prock(object target)
    {
        if (target is CharacterSheet)
        {
            (target as CharacterSheet).DXMod--;
        }
        else
        {
            throw new System.ArgumentException("Cant give a DX debuff to something without DX");
        }
    }

    public override void Unprock(object target)
    {
        if (target is CharacterSheet)
        {
            (target as CharacterSheet).DXMod++;
        }
        else
        {
            throw new System.ArgumentException("Cant take a DX debuff from something without DX");
        }
    }
}

