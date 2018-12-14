using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public PlayerInfo PlayerInfo { get; set; }
    public PlayerOrdinal PlayerNumber { get; set; }
    public bool IsConnected { get; set; }
    public int? ConnectionId { get; set; }

    public void Awake()
    {
        SetDisconnected();
    }

    public void SetConnected(int? cnnId, PlayerOrdinal playerNumber)
    {
        ConnectionId = cnnId;
        PlayerNumber = playerNumber;
        Debug.Log(string.Format("User {0} set as {1}", cnnId, playerNumber));
        IsConnected = true;
    }

    public void SetDisconnected()
    {
        PlayerInfo = new PlayerInfo();
        IsConnected = false;
        ConnectionId = null;
    }

    public bool IsReadyToBattle()
    {
        return IsConnected && PlayerInfo.EquippedDragonId != null;
    }


    public bool CanEquipDragon(string dragonId)
    {
        return DragonCache.TryGetDragonByID(dragonId);
    }

    public void EquipDragon(string dragonId)
    {
        if (CanEquipDragon(dragonId))
        {
            PlayerInfo.EquippedDragonId = dragonId;
            Debug.Log("Equiped dragon " + dragonId);
        }
        else
        {
            Debug.Log("Can't equip dragon " + dragonId);
        }
    }

}
