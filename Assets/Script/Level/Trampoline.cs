using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : Hazard
{
    public float jumpHeight = 1f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource =  GetComponent<AudioSource>();
    }

    public override void OnCollisionWithPlayer(Player2D player)
    {
        player.ForceJump(jumpHeight);
        audioSource.Play();
    }
}
