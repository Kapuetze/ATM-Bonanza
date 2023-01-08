using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KnockBack : Hazard
{
    public float knockBackForce = 100f;
    public float stunTime = 3f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void OnCollisionWithPlayer(Player2D player)
    {
        Vector2 direction = currentCollision.GetContact(0).point - (Vector2)transform.position; 
        player.GoRagdoll();
        player.GetRigidbody().AddForce(direction * knockBackForce, ForceMode2D.Impulse);
        player.transform.DORotate(new Vector3(0, 0, 180), 0.2f);
        StartCoroutine(ResetPlayer(player));

        audioSource.Play();
    }

    protected IEnumerator ResetPlayer(Player2D player)
    {
        yield return new WaitForSeconds(stunTime);
        player.StandUp();
    }
}
