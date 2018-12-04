using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCache : Cache<Card> {



    //public static bool TryGetDragonByID(string id, ref Dragon dragon)
    //{
    //    if (!Instance.Ready)
    //    {
    //        Debug.LogError(string.Format("DragonCache Not Ready: Cannot find Dragon {0}", id));
    //        return false;
    //    }
        
    //    dragon = Instance.Objects.Find(item => item.id == id);
    //    return (dragon != null);
    //}

    public void PrintObjects()
    {
        foreach(Card c in CardCache.Instance.Objects)
        {
            Debug.Log(c.name);
        }
    }
}
