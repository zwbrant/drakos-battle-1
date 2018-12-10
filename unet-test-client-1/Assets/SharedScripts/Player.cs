using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ServerClient {
    public PlayerOrdinal PlayerNumber { get; set; }
    public Dragon EquippedDragon { get; set; }
}
