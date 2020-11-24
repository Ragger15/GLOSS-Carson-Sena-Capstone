using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IQBuff : Effect
{
    public IQBuff()
    {
        ID = 3;
        Name = "IQ + 1";
    }
    public override void Prock(object target)
    {
        if (target is CharacterSheet)
        {
            (target as CharacterSheet).IQMod++;
        }
        else
        {
            throw new System.ArgumentException("Cant give a IQ buff to something without IQ");
        }
    }

    public override void Unprock(object target)
    {
        if (target is CharacterSheet)
        {
            (target as CharacterSheet).IQMod--;
        }
        else
        {
            throw new System.ArgumentException("Cant take a IQ buff from something without IQ");
        }
    }
}
