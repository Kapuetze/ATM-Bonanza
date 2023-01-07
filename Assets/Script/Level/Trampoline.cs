using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : Hazard
{
    public float jumpHeight = 1f;

    public override void OnCollisionWithPlayer(Player2D player)
    {
        player.ForceJump(jumpHeight);
    }
}
