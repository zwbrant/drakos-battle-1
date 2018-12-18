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
    public PlacedDragon DragonState { get; private set; }


    public void SetDragon(Dragon dragon)
    {
        Dragon = dragon;
        DragonState = new PlacedDragon();

        DragonState.DragonId = dragon.id;
        DragonState.HP = (byte)dragon.health;
        DragonState.Attack = (byte)dragon.attack;
        DragonState.Energy = (byte)dragon.energy;

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
            DragonState.HP += (byte)update.DragonHpChange;
        if (update.DragonEnergyChange != null)
            DragonState.Energy += (byte)update.DragonEnergyChange;
        if (update.DragonAttackChange != null)
            DragonState.Attack += (byte)update.DragonAttackChange;

        RefreshTextComponents();
    } 

    public void RefreshTextComponents()
    {
        Name.text = Dragon.name;
        Hp.text = DragonState.HP.ToString();
        Attack.text = DragonState.Attack.ToString();
        Energy.text = DragonState.Energy.ToString();
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
