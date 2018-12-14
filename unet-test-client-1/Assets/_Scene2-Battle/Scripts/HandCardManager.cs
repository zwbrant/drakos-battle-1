using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCardManager : MonoBehaviour {
    public HandCardSet EnabledHandCards;
    public ContainerSet HandCardContainers;
    public ObjectPool HandCardItemPool;
    public StringArrayEvent UpdateCardsEvent;
    public Vector3 LocalSpawnPosition = Vector3.zero;

    public Turn TurnSource;

    public void SpawnCard(Card card)
    {
        // check if 
        ContainerItem container = HandCardContainers.GetFirstEmptyContainerFromLeft();
        if (container == null)
        {
            Debug.Log("Can't spawn card - no empty containers");
            return;
        }

        var cardObj = HandCardItemPool.GetObject();
        cardObj.transform.localPosition = LocalSpawnPosition;

        HandCardItem cardItem = cardObj.GetComponent<HandCardItem>();
        cardItem.transform.SetParent(container.transform, false);
        cardItem.Initialize(card);

        cardItem.gameObject.SetActive(true);
        cardItem.gameObject.name = "Card " + EnabledHandCards.Items.Count;
    }


    public void DrawCards(string[] cardIds)
    {
        for (int i = 0; i < cardIds.Length; i++)
        {
            SpawnCard(CardCache.GetCardById(cardIds[i]));
        }
    }

    public void DiscardCards(string[] cardIds)
    {
        throw new System.NotImplementedException();
    }

    public void ConsumeTurn()
    {
        Debug.Log("HandCardManager: " + TurnSource.name);
        var cardsUpdate = TurnSource.CardsUpdate;

        DrawCards(cardsUpdate.DrawnCards);
    }
}
