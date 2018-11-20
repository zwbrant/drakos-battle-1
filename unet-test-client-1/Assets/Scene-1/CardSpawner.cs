using RoboRyanTron.Unite2017.Sets;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CardSpawner : MonoBehaviour {
    public HandCardSet DisabledCards;
    public RectTransSet CardPlacements;
    public GameObject CardPF;

    public string CardPath { get; private set; }

	// Use this for initialization
	void Start () {
        CardPath = Application.streamingAssetsPath;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnRandomCard()
    {
        SpawnCard(DeserializeRandomCard());
    }

    public void SpawnCard(string cardName)
    {
        Card card = JsonUtilities.DeserializeCardFile(cardName);
        //foreach(HandCardItem item in DisabledCards.SortFromLeftToRight())
        //{
        //    Debug.Log(item.gameObject.name);
        //}
        //SpawnCard(card);    

        SpawnCard(card)
;
    }

    public void SpawnCard(Card card)
    {
        var cardObj = Instantiate(CardPF, CardPlacements.Items[3]);
        HandCardItem cardItem = cardObj.GetComponent<HandCardItem>();
        cardItem.Initialize(card);


        cardItem.gameObject.SetActive(true);
    }

    //public void SetupHandCardItem(HandCardItem cardItem, Card card)
    //{
    //    cardItem.Name.Value = card.name;
    //    cardItem.AttackPower.Value = card.attackPower;
    //    cardItem.EnergyCost.Value = card.energyCost;

    //    string spritePath = "Textures/" + card.spriteFile;


    //    cardItem.Sprite.Value = Resources.Load(spritePath) as Sprite;

    //}

    public HandCardItem GetHandCardFurthestLeft()
    {
        return DisabledCards.SortFromLeftToRight()[0];
    }

    private Card DeserializeRandomCard()
    {
        string[] cardFiles = System.IO.Directory.GetFiles(CardPath, "*.json");
        //for (int i = 0; i < cardFiles.Length; i++)
        //{
        //    Debug.Log(cardFiles[i]);
        //}

        int index = Random.Range(0, cardFiles.Length);

        return JsonUtilities.DeserializeCardFile(Path.GetFileNameWithoutExtension(cardFiles[index]));
    }
}
