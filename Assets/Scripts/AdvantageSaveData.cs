using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AdvantageSaveData
{
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public string Description { get; set; }
    [field: SerializeField] public List<int> EffectIDs { get; set; }

    public AdvantageSaveData()
    {
        Name = "";
        Description = "";
        EffectIDs = new List<int>();

    }

    public AdvantageSaveData(Advantage advantage)
    {
        Name = advantage.Name;
        Description = advantage.Description;
        EffectIDs = advantage.GetEffectIds();
    }

}
