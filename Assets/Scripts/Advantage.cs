using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advantage
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Effect> Effects { get; set; }

    public Advantage()
    {
        Name = "";
        Description = "";
        Effects = new List<Effect>();
    }

    public Advantage(AdvantageSaveData saveData)
    {
        Name = saveData.Name;
        Description = saveData.Description;
        Effects = EffectTracker.IDStoEffects(saveData.EffectIDs);
    }

    public void AddEffect(Effect effect)
    {
        Effects.Add(effect);
    }

    public void RemoveEffect(Effect effect)
    {
        Effects.Remove(effect);
    }

    public void Activate(CharacterSheet sheet)
    {
        foreach(Effect effect in Effects)
        {
            effect.Prock(sheet);
        }
    }

    public void Deactivate(CharacterSheet sheet)
    {
        foreach (Effect effect in Effects)
        {
            effect.Unprock(sheet);
        }
    }
    public List<int> GetEffectIds()
    {
        List<int> ids = new List<int>();

        foreach (Effect effect in Effects)
        {
            ids.Add(effect.ID);
        }

        return ids;
    }

}
