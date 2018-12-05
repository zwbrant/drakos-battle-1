//using RoboRyanTron.Unite2017.Sets;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEngine;

//public class CardSpawner : MonoBehaviour
//{
//    public HandCardSet EnabledCards;
//    public ContainerSet HandCardContainers;
//    public Vector3 LocalSpawnPosition = Vector3.zero;
//    public GameObject CardPF;

//    public string CardPath { get; private set; }

//    // Use this for initialization
//    void Start()
//    {
//        CardPath = Application.streamingAssetsPath + "/Cards/";

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    public void SpawnRandomCard()
//    {
//        SpawnCard(DeserializeRandomCard());
//    }

//    public void SpawnCard(string cardName)
//    {
//        BasicCard card = JsonUtilities.DeserializeCardFile(cardName);

//        SpawnCard(card);
//    }

//    public void SpawnCard(Card card)
//    {
//        // check if 
//        ContainerItem container = HandCardContainers.GetFirstEmptyContainerFromLeft();
//        if (container == null)
//        {
//            Debug.Log("Can't spawn card - no empty containers");
//            return;
//        }

//        var cardObj = Instantiate(CardPF, container.transform);
//        cardObj.transform.localPosition = LocalSpawnPosition;

//        HandCardItem cardItem = cardObj.GetComponent<HandCardItem>();
//        cardItem.Initialize(card);

//        cardItem.gameObject.SetActive(true);
//        cardItem.gameObject.name = "Card " + EnabledCards.Items.Count;
//    }

//    //public void SetupHandCardItem(HandCardItem cardItem, Card card)
//    //{
//    //    cardItem.Name.Value = card.name;
//    //    cardItem.AttackPower.Value = card.attackPower;
//    //    cardItem.EnergyCost.Value = card.energyCost;

//    //    string spritePath = "Textures/" + card.spriteFile;


//    //    cardItem.Sprite.Value = Resources.Load(spritePath) as Sprite;

//    //}


//    private BasicCard DeserializeRandomCard()
//    {
//        string[] cardFiles = System.IO.Directory.GetFiles(CardPath, "*.json");
//        //for (int i = 0; i < cardFiles.Length; i++)
//        //{
//        //    Debug.Log(cardFiles[i]);
//        //}

//        int index = Random.Range(0, cardFiles.Length);

//        return JsonUtilities.DeserializeCardFile(Path.GetFileNameWithoutExtension(cardFiles[index]));
//    }
//}
