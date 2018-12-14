using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCache : Cache<Card> {




    public static Card GetCardById(string id)
    {
        if (!Instance.Ready)
        {
            Debug.LogError(string.Format("DragonCache Not Ready: Cannot find Dragon {0}", id));
            return null;
        }

        return Instance.Objects.Find(item => item.id == id);
    }

    public void PrintObjects()
    {
        foreach(Card c in CardCache.Instance.Objects)
        {
            Debug.Log(c.name);
        }
    }
}
