using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : ManagedBehaviour<PlayerManager> {
    public Player Player { get; private set; }

    public override void Init()
    {
        Player = new Player();
    }


    public void SetEquippedDragon(string dragonId)
    {
        Dragon dragon = new Dragon();
        if (DragonCache.TryGetDragonByID(dragonId, ref dragon))
        {
            Player.EquippedDragon = dragon;
            Debug.Log(string.Format("Player equipped Dragon {0}", dragonId));
        }
        else
        {
            Debug.Log(string.Format("Failed to retrieve Dragon {0} from cache", dragonId));
        }
    }


}
