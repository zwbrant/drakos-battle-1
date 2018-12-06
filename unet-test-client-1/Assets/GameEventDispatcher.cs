using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventDispatcher : MonoBehaviour
{
    public PlayerEvents P1Events;
    public PlayerEvents P2Events;

    [Serializable]
    public struct PlayerEvents
    {
        public UnityStringArrayEvent DrawnCardsEvent;
        public UnityStringArrayEvent DiscardedCardsEvent;
        public UnityStringEvent EquipedDragonEvent;
        public UnityIntEvent DragonDamageEvent;
        public UnityIntEvent CircleRotationEvent;
    }



    public void HandleGameInstanceUpdate(GameInstanceUpdateMsg update)
    {
        // (circle stuff)

        HandlePlayerStateUpdate(update.Player1StateUpdate, P1Events);
        HandlePlayerStateUpdate(update.Player2StateUpdate, P2Events);
    }

    private void HandlePlayerStateUpdate(PlayerStateUpdate update, PlayerEvents player)
    {

        if (player.DrawnCardsEvent != null && update.DrawnCards != null)
            player.DrawnCardsEvent.Invoke(update.DrawnCards);
        if (player.DiscardedCardsEvent != null && update.DiscardedCards != null)
            player.DiscardedCardsEvent.Invoke(update.DiscardedCards);
        if (player.EquipedDragonEvent != null && update.NewDragonEquip != null)
            player.EquipedDragonEvent.Invoke(update.NewDragonEquip);
        if (player.DragonDamageEvent != null && update.DragonDamage != null)
            player.DragonDamageEvent.Invoke(update.DragonDamage);
        if (player.EquipedDragonEvent != null && update.NewDragonEquip != null)
            player.EquipedDragonEvent.Invoke(update.NewDragonEquip);

    }



}
