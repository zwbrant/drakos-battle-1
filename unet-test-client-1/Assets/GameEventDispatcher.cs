using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventDispatcher : MonoBehaviour
{
    public GameStateEvent GameStateEvent;

    public PlayerEvents P1Events;
    public PlayerEvents P2Events;

    [Serializable]
    public struct PlayerEvents
    {
        public StringArrayEvent DrawnCardsEvent;
        public StringArrayEvent DiscardedCardsEvent;
        public StringEvent EquipedDragonEvent;
        public IntEvent DragonDamageEvent;
        public IntEvent CircleRotationEvent;
    }

    public void HandleGameStateUpdate(ClientGameState gameState)
    {
        if (GameStateEvent != null) 
            GameStateEvent.Raise(gameState);
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
            player.DrawnCardsEvent.Raise(update.DrawnCards);
        if (player.DiscardedCardsEvent != null && update.DiscardedCards != null)
            player.DiscardedCardsEvent.Raise(update.DiscardedCards);
        if (player.EquipedDragonEvent != null && update.NewDragonEquip != null)
            player.EquipedDragonEvent.Raise(update.NewDragonEquip);
        if (player.DragonDamageEvent != null && update.DragonHpChange != null)
            player.DragonDamageEvent.Raise(update.DragonHpChange);
        if (player.EquipedDragonEvent != null && update.NewDragonEquip != null)
            player.EquipedDragonEvent.Raise(update.NewDragonEquip);

    }



}
