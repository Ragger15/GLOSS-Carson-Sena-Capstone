using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSaveData
{
    [field: SerializeField] public List<string> SkillNames { get; set; }
    [field: SerializeField] public List<int> EffectIDS { get; set; }
    [field: SerializeField] public bool IsEquipped { get; set; } = true;
    [field: SerializeField] public string Damage { get; set; }
    [field: SerializeField] public float Weight { get; set; }
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public int Value { get; set; }
    [field: SerializeField] public float TL { get; set; }
    [field: SerializeField] public float Quantity { get; set; }

    public ItemSaveData()
    {
        SkillNames = new List<string>();
        EffectIDS = new List<int>();
        IsEquipped = true;
        Damage = "";
        Weight = 0.0f;
        Name = "";
        Value = 0;
        TL = 0.0f;
        Quantity = 0.0f;
    }

    public ItemSaveData(Item item)
    {
        SkillNames = item.SkillNames;
        EffectIDS = item.GetEffectIds();
        IsEquipped = item.IsEquipped;
        Damage = item.Damage;
        Weight = item.Weight;
        Name = item.Name;
        Value = item.Value;
        TL = item.TL;
        Quantity = item.Quantity;
    }

}
