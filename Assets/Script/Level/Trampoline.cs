using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpHeight = 1f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player2D player;
        if(collision.collider.TryGetComponent<Player2D>(out player))
        {
            player.ForceJump(jumpHeight);
        }
    }
}
