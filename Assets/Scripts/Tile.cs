using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tile : MonoBehaviour
{
    [SerializeField] public Coordnaites coordinates;
    [SerializeField] public Color color;
    public Color savedColor;

    public List<Effect> effects = new List<Effect>();
    public List<Pin> pins = new List<Pin>();
    public List<Item> items = new List<Item>();
    public List<Hint> hints = new List<Hint>();
    public List<CharacterSheet> occupants = new List<CharacterSheet>();

    [SerializeField] public bool isReveialed = false;
    [SerializeField] public bool isBlock = false;
    [SerializeField] public bool isVisible = true;

    [SerializeField] public bool Multi = false;
    public bool isBlocked()
    {
        return isBlock;
    }

    public bool IsVisible()
    {
        return isVisible;
    }

    public void SetVisible(bool see)
    {
        isVisible = see;
    }

    public bool IsOccupied()
    {
        bool occupied = false;
        if (occupants.Count > 0)
        {
            occupied = true;
        }
        return occupied;
    }

    public List<CharacterSheet> GetOccupants()
    {
        return occupants;
    }

    public List<Pin> GetPins()
    {
        return pins;
    }

    public List<Item> GetItems()
    {
        return items;
    }

    public List<Hint> GetHints()
    {
        return hints;
    }

    public List<Effect> GetEffects()
    {
        return effects;
    }

    public void Load(TileSaveData saveData)
    {
        color = saveData.Color;
        savedColor = color;
        effects = EffectTracker.IDStoEffects(saveData.EffectIDs);
        pins = saveData.Pins;
        items = new List<Item>();
        foreach (ItemSaveData isd in saveData.Items)
        {
            items.Add(new Item(isd));
        }
        hints = saveData.Hints;
        occupants = new List<CharacterSheet>();
        foreach (CharacterSaveData csd in saveData.Occupants)
        {
            occupants.Add(new CharacterSheet(csd));
        }
        isReveialed = saveData.isReveialed;
        isBlock = saveData.isBlock;
        isVisible = saveData.isVisible;
    }

    public void AddOccupant(CharacterSheet charactersheet)
    {
        occupants.Add(charactersheet);
    }

    public void RemoveOccupant(CharacterSheet charactersheet)
    {
        occupants.Remove(charactersheet);
    }

    public void AddEffect(Effect effect)
    {
        effects.Add(effect);
    }

    public void RemoveEffect(string name)
    {
        foreach (Effect effect in effects)
        {
            if (effect.Name == name) 
            {
                effects.Remove(effect);
                break;
            }
        }
    }

    public void AddPin(Pin pin)
    {
        pins.Add(pin);
    }

    public void RemovePin(string title)
    {
        foreach (Pin pin in pins)
        {
            if (pin.Title == title)
            {
                pins.Remove(pin);
                break;
            }
        }
    }

    public void AddHint(Hint hint)
    {
        hints.Add(hint);
    }

    public void RemoveHint(string title)
    {
        foreach (Hint hint in hints)
        {
            if (hint.Title == title)
            {
                hints.Remove(hint);
                break;
            }
        }
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void RemoveItem (string name)
    {
        foreach (Item item in items)
        {
            if (item.Name == name)
            {
                items.Remove(item);
                break;
            }
        }
    }

    public void ClearEffects()
    {
        effects.Clear();
    }

    public void ClearPins()
    {
        pins.Clear();
    }

    public void ClearHints()
    {
        hints.Clear();
    }

    public void ClearItems()
    {
        items.Clear();
    }

    public void Clear()
    {
        ClearEffects();
        ClearHints();
        ClearItems();
        ClearPins();
    }

    public bool IsReveialed()
    {
        return isReveialed;
    }

    public void Reveial(bool hide)
    {
        isReveialed = hide;
    }

    public void Enter(CharacterSheet entity)
    {
        occupants.Add(entity);
    }

    public void Exit(CharacterSheet entity)
    {
        occupants.Remove(entity);
    }

    public List<int> GetEffectIds()
    {
        List<int> ids = new List<int>();

        foreach (Effect effect in effects)
        {
            ids.Add(effect.ID);
        }

        return ids;
    }

    public List<ItemSaveData> GetItemSaveData()
    {
        List<ItemSaveData> ids = new List<ItemSaveData>();

        foreach (Item item in items)
        {
            ids.Add(item.GetSaveData());
        }

        return ids;
    }

    public List<CharacterSaveData> GetOccupantSaveData()
    {
        List<CharacterSaveData> ids = new List<CharacterSaveData>();

        foreach (CharacterSheet sheet in occupants)
        {
            ids.Add(sheet.GetSaveData());
        }

        return ids;
    }

    public TileSaveData GetSaveData()
    {
        return new TileSaveData(this);
    }

}
