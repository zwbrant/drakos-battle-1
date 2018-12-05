using RoboRyanTron.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandCardItem : PooledObject {
    public HandCardSet EnabledCards;
    public HandCardSet DisabledCards;

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

        Name.text = Card.name;
        AttackPower.text = Card.power.ToString();
        EnergyCost.text = Card.cost.ToString();

        //string spritePath = "Textures/" + Card.spriteFile;
        //Sprite.sprite = Resources.Load<Sprite>(spritePath);
    }

    private void OnEnable()
    {
        EnabledCards.Add(this);

        DisabledCards.Remove(this);
    }

    private void OnDisable()
    {
        DisabledCards.Add(this);

        EnabledCards.Remove(this);
    }
}
