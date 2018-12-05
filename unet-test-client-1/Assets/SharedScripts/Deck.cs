using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Deck
{

    public Stack<Card> Cards { get; private set; }

    public Deck(Cache<Card> cardCache, int deckSize, bool allowDuplicates) 
    {
        if (!cardCache.Ready)
        {
            throw new Exception("Can't build this Deck because the given card cache isn't ready");
        }
        if (cardCache.Objects.Count < deckSize)
        {
            throw new Exception(
                string.Format("Can't build a Deck of size {0} because the given cache only contains {1} cards", deckSize, cardCache.Objects.Count));
        }


        
        for (int i = 0; i < deckSize; i++)
        {
            Cards.Push(cardCache.Objects[UnityEngine.Random.Range(0, cardCache.Objects.Count - 1)]);
        }
    }
}
