using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IQDebuff : Effect
{
    public IQDebuff()
    {
        ID = 7;
        Name = "IQ - 1";
    }
    public override void Prock(object target)
    {
        if (target is CharacterSheet)
        {
            (target as CharacterSheet).IQMod--;
        }
        else
        {
            throw new System.ArgumentException("Cant give a IQ debuff to something without IQ");
        }
    }

    public override void Unprock(object target)
    {
        if (target is CharacterSheet)
        {
            (target as CharacterSheet).IQMod++;
        }
        else
        {
            throw new System.ArgumentException("Cant take a IQ debuff from something without IQ");
        }
    }
}
