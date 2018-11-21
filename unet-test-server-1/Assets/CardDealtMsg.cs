using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardDealtMsg : NetMsg {

    public string CardName { get; set; }

    public CardDealtMsg()
    {
        OP = NetOP.CardDealt;
    } 
}
