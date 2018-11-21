using RoboRyanTron.Unite2017.Sets;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu]
public class RectTransSet : RuntimeSet<RectTransform> {
    public List<RectTransform> SortFromLeftToRight()
    {
        return Items.OrderBy(item => item.transform.position.x).ToList();
    }
}
