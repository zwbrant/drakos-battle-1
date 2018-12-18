using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClientGameState : byte {
    AwaitingEnemyJoin,
    SetupPhase,
    PlayerSetupReady,
    PlayerTurn,
    PlayerAttack,
    EnemyTurn,
    EnemyAttack
}
