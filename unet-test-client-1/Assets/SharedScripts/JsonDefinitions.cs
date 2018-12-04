using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dragon {
    public string id { get; set; }
    public string name { get; set; }
    public int level { get; set; }
    public int health { get; set; }
    public int attack { get; set; }
    public int attack_cost { get; set; }
    public int energy { get; set; }
    public string age { get; set; }
    public string color { get; set; }
    public string spriteFile { get; set; }

    public List<Ability> abilities { get; set; }
}

[System.Serializable]
public class Ability
{
    public string slug { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int cost { get; set; }
    public int cooldown { get; set; }
    public int chance { get; set; }
    public string target { get; set; }
    public string player { get; set; }
    public string type { get; set; }

    public Action action { get; set; }
}

[System.Serializable]
public class Action
{
    public string type { get; set; }
    public string mod { get; set; }
    public string trigger { get; set; }
    public float value { get; set; }
}

public class Card
{
    public string id { get; set; }
    public string slug { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string type { get; set; }
    public string subtype { get; set; }
    public int cost { get; set; }
    public string color { get; set; }
    public int power { get; set; }

    public Ability ability { get; set; }
}