using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCardManager : MonoBehaviour {
    public HandCardSet EnabledHandCards;
    public ContainerSet HandCardContainers;
    public ObjectPool HandCardItemPool;
    public Vector3 LocalSpawnPosition = Vector3.zero;

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
        cardItem.Initialize(card);

        cardItem.gameObject.SetActive(true);
        cardItem.gameObject.name = "Card " + EnabledHandCards.Items.Count;
    }

}
