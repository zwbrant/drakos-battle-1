using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardContainerItem : MonoBehaviour
{
    public CardContainerSet Set;

    public bool IsEmpty
    {
        get { return transform.childCount < 1; }
    }

    private void OnEnable()
    {
        Set.Add(this);
    }

    private void OnDisable()
    {
        Set.Remove(this);
    }
}
