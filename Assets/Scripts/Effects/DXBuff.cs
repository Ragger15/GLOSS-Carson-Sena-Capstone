using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DXBuff : Effect
{
    public DXBuff()
    {
        ID = 2;
        Name = "DX + 1";
    }
    public override void Prock(object target)
    {
        if (target is CharacterSheet)
        {
            (target as CharacterSheet).DXMod++;
        }
        else
        {
            throw new System.ArgumentException("Cant give a DX buff to something without DX");
        }
    }

    public override void Unprock(object target)
    {
        if (target is CharacterSheet)
        {
            (target as CharacterSheet).DXMod--;
        }
        else
        {
            throw new System.ArgumentException("Cant take a DX buff from something without DX");
        }
    }
}
