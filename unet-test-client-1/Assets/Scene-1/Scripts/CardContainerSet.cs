using RoboRyanTron.Unite2017.Sets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class CardContainerSet : RuntimeSet<CardContainerItem> {

    public List<CardContainerItem> SortFromLeftToRight()
    {
        return Items.OrderBy(item => item.transform.position.x).ToList();
    }

    public CardContainerItem GetFirstEmptyContainerFromLeft()
    {
        var items = Items.OrderBy(item => item.transform.position.x).Where(item => item.IsEmpty);

        if (items.Count() < 1)
            return null;
        else
            return items.First();
    }
}
