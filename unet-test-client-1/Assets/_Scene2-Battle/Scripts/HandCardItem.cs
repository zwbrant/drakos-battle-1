using RoboRyanTron.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandCardItem : PooledObject {
    public HandCardSet EnabledCards;

    public Card Card { get; private set; }

    public Text Name;
    public Image Sprite;
    public Text AttackPower;
    public Text EnergyCost;

    private void Start()
    {

    }

    public void Initialize(Card card)
    {
        Card = card;

        if (Name != null)
            Name.text = Card.name;
        if (AttackPower != null)
            AttackPower.text = Card.power.ToString();
        if (EnergyCost != null)
            EnergyCost.text = Card.cost.ToString();
        if (Sprite != null)
        {
            string spritePath = "Textures/" + CardSpriteCache.Instance.Objects.Find(item => item.id == card.id).spriteFile;
            Sprite.sprite = Resources.Load<Sprite>(spritePath);
        }


    }

    private void OnEnable()
    {
        EnabledCards.Add(this);
    }

    private void OnDisable()
    {
        EnabledCards.Remove(this);
    }
}
