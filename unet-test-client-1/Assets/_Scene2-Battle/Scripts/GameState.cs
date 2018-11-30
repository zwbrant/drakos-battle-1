using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState : byte {
    AwaitingPlayerJoin,
    SetupCards,
    UserSetupReady,
    UserTurn,
    UserAttack,
    OpponentTurn,
    OpponentAttack
}
