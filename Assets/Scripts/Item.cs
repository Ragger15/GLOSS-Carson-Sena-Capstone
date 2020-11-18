using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public List<string> SkillNames { get; set; }
    public List<Effect> Effects { get; set; }
    public bool IsEquipped { get; set; } = true;
    public string Damage { get; set; }
    public float Weight { get; set; }
    public string Name { get; set; }
    public int Value { get; set; }
    public float TL { get; set; }
    public float Quantity { get; set; }

    public Item()
    {
        SkillNames = new List<string>();
        Effects = new List<Effect>();
        IsEquipped = true;
        Damage = "";
        Weight = 0.0f;
        Name = "";
        Value = 0;
        TL = 0.0f;
        Quantity = 0.0f;
    }

    public Item(ItemSaveData saveData)
    {
        SkillNames = saveData.SkillNames;
        Effects = EffectTracker.IDStoEffects(saveData.EffectIDS);
        IsEquipped = saveData.IsEquipped;
        Damage = saveData.Damage;
        Weight = saveData.Weight;
        Name = saveData.Name;
        Value = saveData.Value;
        TL = saveData.TL;
        Quantity = saveData.Quantity;
    }

    public void AddSkillName(string name)
    {
        SkillNames.Add(name);
    }

    public void RemoveSkillName(string name)
    {
        SkillNames.Remove(name);
    }

    public void AddEffect(Effect effect)
    {
        Effects.Add(effect);
    }

    public void RemoveEffect(Effect effect)
    {
        Effects.Remove(effect);
    }

    public void SetName(string nme)
    {
        Name = nme;
    }

    public void SetValue(string vlue)
    {
        Value = int.Parse(vlue);
    }

    public void SetTL(string TLL)
    {
        TL = float.Parse(TLL);
    }

    public void SetQuantity(string qntituy)
    {
        Quantity = float.Parse(qntituy);
    }

    public void RemoveEffect(string name)
    {
        foreach (Effect effect in Effects)
        {
            if (effect.Name == name)
            {
                Effects.Remove(effect);
                break;
            }
        }
    }

    public void Activate()
    {
        foreach (Effect effect in Effects)
        {
            effect.Prock(this);
        }
    }

    public void Deactivate()
    {
        foreach (Effect effect in Effects)
        {
            effect.Unprock(this);
        }
    }

    public ItemSaveData GetSaveData()
    {
        //ItemSaveData saveData = new ItemSaveData();

        //saveData.SkillNames = SkillNames;
        //saveData.Effects = Effects;
        //saveData.IsEquipped ;
        //saveData.Damage;
        //saveData.Weight;
        //saveData.Name;
        //saveData.Value;
        //saveData.TL;
        //saveData.Quantity;

        return new ItemSaveData(this);
    }

    public List<int> GetEffectIds()
    {
        List<int> ids = new List<int>();

        foreach(Effect effect in Effects)
        {
            ids.Add(effect.ID);
        }

        return ids;
    }

}
