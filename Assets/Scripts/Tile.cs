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

    private List<Effect> effects = new List<Effect>();
    private List<Pin> pins = new List<Pin>();
    private List<Item> items = new List<Item>();
    private List<Hint> hints = new List<Hint>();
    private List<CharacterSheet> occupants = new List<CharacterSheet>();

    [SerializeField] private Effect[] effectsArray;
    [SerializeField] private Pin[] pinsArray;
    [SerializeField] private Item[] itemsArray;
    [SerializeField] private Hint[] hintsArray;
    [SerializeField] private CharacterSheet[] occupantsArray;

    [SerializeField] private bool isReveialed = false;
    [SerializeField] private bool isBlock = false;
    [SerializeField] private bool isVisible = true;

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

    public void UpdateForSave()
    {
        effectsArray = effects.ToArray();
        pinsArray = pins.ToArray();
        itemsArray = items.ToArray();
        hintsArray = hints.ToArray();
        occupantsArray = occupants.ToArray();

    }

    //public string ToJsonString()
    //{
    //    string json = "{\"coordinates\":" + coordinates + ",\"color\":" + color + ",\"pinsArray\":[";

    //    for (int i = 0; i < pinsArray.Length; i++)
    //    {
    //        json += JsonUtility.ToJson(pinsArray[i]);

    //        if (i + 1 < pinsArray.Length)
    //        {
    //            json += ",";
    //        }
    //        else
    //        {
    //            json += "],\"itemsArray\":[";
    //        }

    //    }

    //    for (int i = 0; i < itemsArray.Length; i++)
    //    {
    //        json += JsonUtility.ToJson(itemsArray[i]);

    //        if (i + 1 < itemsArray.Length)
    //        {
    //            json += ",";
    //        }
    //        else
    //        {
    //            json += "],\"hintsArray\":[";
    //        }

    //    }

    //    for (int i = 0; i < hintsArray.Length; i++)
    //    {
    //        json += JsonUtility.ToJson(hintsArray[i]);
    //        if (i + 1 < hintsArray.Length)
    //        {
    //            json += ",";
    //        }
    //        else
    //        {
    //            json += "]";
    //        }

    //    }
    //    json += "}";

    //    return json;
    //}

    //public static Tile FromJsonString(String json)
    //{

    //}
}
