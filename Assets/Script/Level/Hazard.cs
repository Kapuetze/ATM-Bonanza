using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hazard : MonoBehaviour
{
    protected Collision2D currentCollision;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Player2D player;
        if (collision.collider.TryGetComponent<Player2D>(out player))
        {
            currentCollision = collision;
            OnCollisionWithPlayer(player);
            print("Hit player");
        }
    }

    public virtual void OnCollisionWithPlayer(Player2D player)
    {

    }
}
