using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pump : MonoBehaviour
{
    public TreeLogic tree;
    public ParticleSystem waterEffect;
    public float downWardLimit = 0.3f;
    public float speed = 1f;
    public bool release = true;

    private Player2D player;
    private Vector3 startPos;
    private float dist = 0f;
    // Start is called before the first frame update

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(release && transform.position != startPos)
        {
            // Reset position
            transform.position = Vector3.Lerp(transform.position, startPos, speed);
        }

        if (transform.localPosition.y > downWardLimit + 0.05f && !release)
        {
            // Move handle down
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, downWardLimit, 0), speed);
            tree.WaterTree();
        }
        else if(!release)
        {
            waterEffect.Stop();
            //if (audioSource.isPlaying)
            //    audioSource.Stop();

            print(downWardLimit + 0.05f);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent<Player2D>(out player) && release)
        {
            waterEffect.Play();
            if (!audioSource.isPlaying)
                audioSource.Play();

            release = false;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Player2D>(out player) && !release)
        {
            waterEffect.Stop();

            //if (audioSource.isPlaying)
            //    audioSource.Stop();

            release = true;
            player = null;
        }
    }
}
