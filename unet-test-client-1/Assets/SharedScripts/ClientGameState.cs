using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClientGameState : byte {
    AwaitingOpponentJoin,
    SetupPhase,
    PlayerSetupReady,
    PlayerTurn,
    PlayerAttack,
    OpponentTurn,
    OpponentAttack
}
