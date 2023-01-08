using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLogic : MonoBehaviour
{
    [SerializeField]
    private float SPAWN_INTERVAL = 0.3f;
    [SerializeField]
    private float GROW_SPOT_COOLDOWN = 10.0f;
    [SerializeField]
    private float INITIAL_SPAWN_COOLDOWN = 5.0f;

    public List<GrowSpot> spots = new List<GrowSpot>();

    [SerializeField]
    private GameObject moneyPrefab;

    private bool harvestAllowed = false;

    private AudioSource audio;

    #nullable enable
    private GameObject? player;
    #nullable disable

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();

        InvokeRepeating("CheckTreeStatus", GROW_SPOT_COOLDOWN, SPAWN_INTERVAL);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            HarvestTree();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            harvestAllowed = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = null;
            harvestAllowed = false;
        }
    }

    void CheckTreeStatus()
    {
        foreach(GrowSpot growSpot in spots)
        {
            // Check if grow spot is empty and cooldown exceeded
            if(!growSpot.money && (growSpot.cooldown == 0f || Time.time > growSpot.cooldown))
            {
                growSpot.money = Instantiate(moneyPrefab, growSpot.transform).GetComponent<Money>();
            }
        }
    }

    public void HarvestTree()
    {
        if(harvestAllowed == true)
        {
            Vector3 position = player.transform.position;

            #nullable enable
            GrowSpot? closestGrowSpot = null;
            #nullable disable

            float minDistance = Mathf.Infinity;
            float currentDistance = 0f;
            // Find the growSpot that is closest to the player position
            foreach (GrowSpot growSpot in spots)
            {
                if(growSpot.money != null)
                {
                    currentDistance = Vector3.Distance(growSpot.money.transform.position, position);
                    if(currentDistance < minDistance)
                    {
                        minDistance = currentDistance;
                        closestGrowSpot = growSpot;
                    }
                }
            }

            if(closestGrowSpot != null)
            {
                closestGrowSpot.money.StopGrowing();
                closestGrowSpot.money = null;
                closestGrowSpot.cooldown = Time.time + GROW_SPOT_COOLDOWN;

                audio.Play();
            }
        }
    }
}
