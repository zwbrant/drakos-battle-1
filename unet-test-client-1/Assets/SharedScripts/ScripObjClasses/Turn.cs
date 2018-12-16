using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu]
public class Turn : ScriptableObject
{
    public bool HasBeenConsumed { get; set; }

    public byte? TurnNumber { get; set; }
    public PlayerOrdinal PlayerNumber { get; set; }

    public DragonStateUpdate DragonUpdate { get; set; }
    public CirclesStateUpdate CirclesUpdate { get; set; }
    public CardsStateUpdate CardsUpdate { get; set; }
}

