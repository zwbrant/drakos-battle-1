using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CircleItem : PooledObject {
    public CircleSet EnabledCircles;

    // Component references
    public Image BackgroundSprite;
    public Image LeftColor;
    public Image RightColor;
    public TextMeshProUGUI PowerText;

    // fungible datamodels to describe circle and its state
    public Circle Circle { get; private set; }
    public PlacedCard CardState { get; private set; }

    // cached version of full Card datamodel that the CardState derives from
    public Card Card { get; private set; }



    // Use this for initialization
    void Awake () {
        if (Circle == null)
            Circle = new Circle();
        if (CardState == null)
            CardState = new PlacedCard();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetCard(Card card)
    {
        PowerText.gameObject.SetActive(true);

        Card = card;
        CardState = new PlacedCard() {
            CardId = card.id,
            Power = (byte)card.power
        };

        RefreshTextComponents();
        RefreshBackgroundSprite();
    }

    public void UpdateCircle(CircleUpdate update)
    {
        if (update.NewCard != null)
        {
            Card newCard = CardCache.GetCardById(update.NewCard.CardId);
            SetCard(newCard);
        }

        if (update.RightColor != null)
            Circle.RightColor = update.RightColor.Value;
        if (update.LeftColor != null)
            Circle.LeftColor = update.LeftColor.Value;
        if (update.CardPowerChange != null)
            CardState.Power = (byte)((int)CardState.Power + (int)update.CardPowerChange);

        RefreshTextComponents();
        RefreshColors();
        RefreshBackgroundSprite();
    }

    public void SetEmpty(Sprite background, Color backgroundColor, Color circleColor)
    {
        if (Circle == null)
            Circle = new Circle();

        BackgroundSprite.sprite = background;
        BackgroundSprite.color = backgroundColor;
        RightColor.color = circleColor;
        LeftColor.color = circleColor;

        PowerText.gameObject.SetActive(false);

    }

    public void SetIndex(int index)
    {
        if (Circle == null)
            Circle = new Circle();

        Circle.CircleIndex = (byte)index;
    }


    public void RefreshTextComponents()
    {
        PowerText.text = CardState.Power.ToString();
    }

    public void RefreshColors()
    {
        Color color;
        ColorUtility.TryParseHtmlString(((CircleColor)Circle.RightColor).ToString(), out color);
        
        RightColor.color = color;

        if (Circle.LeftColor != null)
        {
            ColorUtility.TryParseHtmlString(((CircleColor)Circle.LeftColor).ToString(), out color);
        }
        LeftColor.color = color;
    }

    public void RefreshBackgroundSprite()
    {
        if (Card != null)
        {
            string spritePath = "Textures/" + CardSpriteCache.Instance.Objects.Find(item => item.id == Card.id).spriteFile;
            BackgroundSprite.sprite = Resources.Load<Sprite>(spritePath);
        }
    }

    private void OnEnable()
    {
        EnabledCircles.Add(this);
    }

    private void OnDisable()
    {
        EnabledCircles.Remove(this);
    }
}
