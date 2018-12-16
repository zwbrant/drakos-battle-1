using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlingDragon : MonoBehaviour {


    // Component references
    public Text Name;
    public Image Sprite;
    public Text Hp;
    public Text Attack;
    public Text Energy;

    public Turn TurnSource;

    public Dragon Dragon { get; private set; }
    public int CurrHp { get; private set; }
    public int CurrAttack { get; private set; }
    public int CurrEnergy { get; private set; }


    public void SetDragon(Dragon dragon)
    {
        Dragon = dragon;
        CurrHp = dragon.health;
        CurrAttack = dragon.attack;
        CurrEnergy = dragon.energy;

        RefreshTextComponents();
        RefreshSpriteComponent();
    }

    public void UpdateDragonState(DragonStateUpdate update)
    {
        if (update.NewDragonEquip != null)
        {
            Dragon newDragon = DragonCache.GetDragonByID(update.NewDragonEquip);
            if (newDragon == null)
                Debug.LogError("Couldn't find dragon " + update.NewDragonEquip + " in cache");
            else
            {
                SetDragon(newDragon);
            }
        }

        if (update.DragonHpChange != null)
            CurrHp += (int)update.DragonHpChange;
        if (update.DragonEnergyChange != null)
            CurrEnergy += (int)update.DragonEnergyChange;
        if (update.DragonAttackChange != null)
            CurrAttack += (int)update.DragonAttackChange;

        RefreshTextComponents();
    } 

    public void RefreshTextComponents()
    {
        Name.text = Dragon.name;
        Hp.text = CurrHp.ToString();
        Attack.text = CurrAttack.ToString();
        Energy.text = CurrEnergy.ToString();
    }

    public void RefreshSpriteComponent()
    {
        string spritePath = "Textures/" + Dragon.spriteFile;
        Sprite.sprite = Resources.Load<Sprite>(spritePath);
    }

    public void ConsumeTurn()
    {
        Debug.Log("BattlingDragon: " + TurnSource.name);
        var dragonUpdate = TurnSource.DragonUpdate;

        UpdateDragonState(dragonUpdate);
    }
}
