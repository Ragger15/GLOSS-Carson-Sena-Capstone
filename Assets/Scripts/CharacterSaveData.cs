using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterSaveData
{
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public List<Skill> Skills { get; set; }
    [field: SerializeField] public List<ItemSaveData> Items { get; set; }
    [field: SerializeField] public List<AdvantageSaveData> Advantages { get; set; }

}
