using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileSaveData
{
    [field: SerializeField] public Color Color { get; set; }
    [field: SerializeField] public List<int> EffectIDs {get; set; }
    [field: SerializeField] public List<Pin> Pins {get; set; }
    [field: SerializeField] public List<ItemSaveData> Items {get; set; }
    [field: SerializeField] public List<Hint> Hints {get; set; }
    [field: SerializeField] public List<CharacterSaveData> Occupants {get; set; }

    [field: SerializeField] public bool isReveialed {get; set; }
    [field: SerializeField] public bool isBlock {get; set; }
    [field: SerializeField] public bool isVisible {get; set; }

    public TileSaveData()
    {
        Color = new Color();
        EffectIDs = new List<int>();
        Pins = new List<Pin>();
        Items = new List<ItemSaveData>();
        Hints = new List<Hint>();
        Occupants = new List<CharacterSaveData>();
        isReveialed = false;
        isBlock = false;
        isVisible = true;
    }

    public TileSaveData(Tile tile)
    {
        Color = tile.color;
        EffectIDs = tile.GetEffectIds();
        Pins = tile.pins;
        Items = tile.GetItemSaveData();
        Hints = tile.hints;
        Occupants = tile.GetOccupantSaveData();
        isReveialed = tile.isReveialed;
        isBlock = tile.isBlock;
        isVisible = tile.isVisible;
    }

}
