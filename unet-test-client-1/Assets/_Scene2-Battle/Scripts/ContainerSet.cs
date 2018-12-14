using RoboRyanTron.Unite2017.Sets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class ContainerSet : RuntimeSet<ContainerItem> {

    public List<ContainerItem> SortFromLeftToRight()
    {
        return Items.OrderBy(item => item.transform.position.x).ToList();
    }

    public ContainerItem GetFirstEmptyContainerFromLeft()
    {
        var items = Items.OrderBy(item => item.transform.position.x).Where(item => item.IsEmpty);

        if (items.Count() < 1)
            return null;
        else
            return items.First();
    }

    public bool AreAllContainersEmpty()
    {
        foreach(ContainerItem item in Items)
        {
            if (!item.IsEmpty)
                return false;
        }
        return true;
    }
}
