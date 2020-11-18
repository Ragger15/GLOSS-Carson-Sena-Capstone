using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EffectTracker
{
    public static Effect IDToEffect(int id)
    {
        Effect answer;
        switch (id)
        {
            case 1:
                answer = new STBuff();
                break;
            default:
                throw new System.ArgumentException($"{id} is not a valid Effect ID");
        }

        return answer;
    }

    public static List<Effect> IDStoEffects(List<int> ids)
    {
        List<Effect> effects = new List<Effect>();

        foreach(int id in ids)
        {
            effects.Add(IDToEffect(id));
        }

        return effects;
    }
}
