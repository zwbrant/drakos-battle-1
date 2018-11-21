using RoboRyanTron.Unite2017.Sets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item<T> : MonoBehaviour {
    public RuntimeSet<T> Set;
}
