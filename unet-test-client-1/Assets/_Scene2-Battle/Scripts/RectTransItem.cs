using RoboRyanTron.Unite2017.Sets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTransItem : SetItem<RectTransform> {

    public RectTransSet Set;
    public override RuntimeSet<RectTransform> SetProp {
        get {
            return Set;
        }
        set { Set = (RectTransSet)value; } }

}
