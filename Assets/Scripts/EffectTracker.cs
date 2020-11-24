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
            case 0:
                answer = new Damage();
                break;
            case 1:
                answer = new STBuff();
                break;
            case 2:
                answer = new DXBuff();
                break;
            case 3:
                answer = new IQBuff();
                break;
            case 4:
                answer = new HTBuff();
                break;
            case 5:
                answer = new STDebuff();
                break;
            case 6:
                answer = new DXDebuff();
                break;
            case 7:
                answer = new IQDebuff();
                break;
            case 8:
                answer = new HTDebuff();
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
