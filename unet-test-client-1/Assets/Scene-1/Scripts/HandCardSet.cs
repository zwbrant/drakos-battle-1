using RoboRyanTron.Unite2017.Sets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu]
public class HandCardSet : RuntimeSet<HandCardItem> {

    public List<HandCardItem> SortFromLeftToRight()
    {
        return Items.OrderBy(item => item.transform.position.x).ToList();
    }
}
