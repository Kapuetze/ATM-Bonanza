using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pump : MonoBehaviour
{
    public float downWardLimit = 0.3f;
    public float speed = 1f;
    public bool release = true;
    public float dist = 0f;

    private Player2D player;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, transform.parent.position);
        if(release && transform.position != startPos)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, speed);
        }


        if (dist > downWardLimit && !release)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, downWardLimit, 0), speed);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent<Player2D>(out player))
        {
            release = false;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Player2D>(out player))
        {
            release = true;
            player = null;
        }
    }
}
