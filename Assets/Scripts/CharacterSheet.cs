using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheet
{

    public enum Encumerance
    {
        NO,
        LIGHT,
        MEDIUM,
        HEAVY,
        XH,
        OVER
    }

    public string Name { get; set; }
    public List<Skill> Skills { get; set; }
    public List<Item> Items { get; set; }
    public List<Advantage> Advantages { get; set; }

    public List<Action> Actions { get; set; }

    public Encumerance CurrentEncum { get; }
    private int peakMove = 0;

    public int STMod { get; set; } = 0;
    public int ST
    {
        get { return 10 + STMod; }
    }

    public int DXMod { get; set; } = 0;
    public int DX
    {
        get { return 10 + DXMod; }
    }
    public int IQMod { get; set; } = 0;
    public int IQ
    {
        get { return 10 + IQMod; }
    }
    public int HTMod { get; set; } = 0;
    public int HT
    {
        get { return 10 + HTMod; }
    }

    public int thrustMod { get; set; } = 0;

    public string GetThrust()
    {
        float numOfDice = ((ST-10)+4) / 8.0f;
        int moddifier = thrustMod;
        float decimals = numOfDice % 1;
        numOfDice -= decimals;

        if(decimals > .75)
        {
            numOfDice++;
        }
        else if(decimals > .5)
        {
            numOfDice++;
            moddifier--;
        }
        else if(decimals > .25)
        {
            moddifier += 2;
        }
        else if(decimals > 0)
        {
            moddifier++;
        }


        return $"{numOfDice}D{moddifier}";
    }

    public int swingMod { get; set; } = 0;

    public string GetSwing()
    {
        float numOfDice = ((ST - 10) + 4) / 4.0f;
        int moddifier = swingMod;
        float decimals = numOfDice % 1;
        numOfDice -= decimals;

        if (decimals > .75)
        {
            numOfDice++;
        }
        else if (decimals > .5)
        {
            numOfDice++;
            moddifier--;
        }
        else if (decimals > .25)
        {
            moddifier += 2;
        }
        else if (decimals > 0)
        {
            moddifier++;
        }


        return $"{numOfDice}D{moddifier}";
    }

    public float basicspeedMod { get; set; } = 0;

    public float GetBasicSpeed()
    {
        return ((HT + DX) / 4.0f) + basicspeedMod;
    }

    public int moveMod {get; set;} = 0;

    public int GetMove()
    {
        return int.Parse((GetBasicSpeed() - (GetBasicSpeed() % 1)).ToString()) + moveMod;
    }

    public int passiveDefence {get; set;} = 0;
    public int dodgeMod {get; set;} = 0;

    public int GetDodge()
    {
        return GetMove() + dodgeMod;
    }

    public int healthMod { get; set; } = 0;

    public int GetHealth()
    {
        return HT + healthMod;
    }


    public CharacterSheet(CharacterSaveData saveData)
    {
        Name = saveData.Name;
        Skills = saveData.Skills;
        Items = new List<Item>();
        foreach (ItemSaveData isd in saveData.Items)
        {
            Items.Add(new Item(isd));
        }
        Advantages = new List<Advantage>();
        foreach (AdvantageSaveData asd in saveData.Advantages)
        {
            Advantages.Add(new Advantage(asd));
        }

        ActivateAdvantages();
        ActivateItems();
        CalcEncumberance();

    }

    public CharacterSheet()
    {
        Name = "";
        Skills = new List<Skill>();
        Items = new List<Item>();
        Advantages = new List<Advantage>();
    }

    public void ActivateAdvantages()
    {
        foreach(Advantage advantage in Advantages)
        {
            advantage.Activate(this);
        }
    }

    public void DeactivateAdvantages()
    {
        foreach (Advantage advantage in Advantages)
        {
            advantage.Deactivate(this);
        }
    }

    public void AddAdvantage(Advantage advantage)
    {
        Advantages.Add(advantage);
        advantage.Activate(this);
    }

    public void RemoveAdvantage(Advantage advantage)
    {
        Advantages.Remove(advantage);
        advantage.Deactivate(this);
    }

    private void SetEncumberanceMod(Encumerance encum)
    {
        switch (CurrentEncum)
        {
            case Encumerance.NO:
                break;
            case Encumerance.LIGHT:
                moveMod++;
                break;
            case Encumerance.MEDIUM:
                moveMod += 2;
                break;
            case Encumerance.HEAVY:
                moveMod += 3;
                break;
            case Encumerance.XH:
                moveMod += 4;
                break;
            case Encumerance.OVER:
                moveMod += peakMove;
                break;
        }
        peakMove = GetMove();
        switch (encum)
        {
            case Encumerance.NO:
                break;
            case Encumerance.LIGHT:
                moveMod--;
                break;
            case Encumerance.MEDIUM:
                moveMod -= 2;
                break;
            case Encumerance.HEAVY:
                moveMod -= 3;
                break;
            case Encumerance.XH:
                moveMod -= 4;
                break;
            case Encumerance.OVER:
                moveMod -= peakMove;
                break;
        }
    }

    public void CalcEncumberance()
    {
        float totalWeight = 0;
        Encumerance answer = Encumerance.NO;
        foreach(Item item in Items)
        {
            if(item.IsEquipped)
            {
                totalWeight += item.Weight;
            }
        }

        if(totalWeight > ST * 30)
        {
            answer = Encumerance.OVER;
        }
        else if (totalWeight > ST * 20)
        {
            answer = Encumerance.XH;
        }
        else if (totalWeight > ST * 12)
        {
            answer = Encumerance.HEAVY;
        }
        else if (totalWeight > ST * 6)
        {
            answer = Encumerance.MEDIUM;
        }
        else if (totalWeight > ST * 4)
        {
            answer = Encumerance.LIGHT;
        }

        SetEncumberanceMod(answer);

    }

    public void AddSkill(Skill skill)
    {
        Skills.Add(skill);
    }

    public void RemoveSkill(Skill skill)
    {
        Skills.Remove(skill);
    }


    public void AddItem(Item item)
    {
        Items.Add(item);
        item.Activate();
        CalcEncumberance();
    }

    public void RemoveItem(Item item)
    {
        Items.Remove(item);
        item.Deactivate();
        CalcEncumberance();
    }

    public void ActivateItems()
    {
        foreach (Item item in Items)
        {
            item.Activate();
        }
    }

    public void DeactivateItems()
    {
        foreach (Item item in Items)
        {
            item.Deactivate();
        }
    }

    public void AddAction(Action action)
    {
        Actions.Add(action);
    }

    public void RemoveAction(Action action)
    {
        Actions.Remove(action);
    }

    public CharacterSaveData GetSaveData()
    {
        CharacterSaveData saveData = new CharacterSaveData();

        saveData.Name = Name;
        saveData.Skills = Skills;
        saveData.Items = new List<ItemSaveData>();
        foreach (Item item in Items)
        {
            saveData.Items.Add(new ItemSaveData(item));
        }
        saveData.Advantages = new List<AdvantageSaveData>();
        foreach (Advantage advantage in Advantages)
        {
            saveData.Advantages.Add(new AdvantageSaveData(advantage));
        }

        return saveData;
    }

}
