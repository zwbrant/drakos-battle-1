using RoboRyanTron.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandCardItem : MonoBehaviour {
    public bool DisableOnStart = false;

    public HandCardSet EnabledCards;
    public HandCardSet DisabledCards;

    public Text Name;
    public Image Sprite;
    public Text AttackPower;
    public Text EnergyCost;

    private void Start()
    {
        if (DisableOnStart)
            gameObject.SetActive(false);
    }

    public void Initialize(Card card)
    {
        Name.text = card.name;
        AttackPower.text = card.attackPower.ToString();
        EnergyCost.text = card.energyCost.ToString();

        string spritePath = "Textures/" + card.spriteFile + ".jpg";

        Sprite.sprite = Resources.Load("Textures/witch") as Sprite;

        gameObject.SetActive(true);
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
