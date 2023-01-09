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
    [SerializeField]
    private float WATER_TIMER_INCREASE = 0.1f;

    public List<GrowSpot> spots = new List<GrowSpot>();

    [SerializeField]
    private GameObject moneyPrefab;

    private bool harvestAllowed = false;

    private AudioSource audio;

    #nullable enable
    private GameObject? player;
    private GrowSpot? closestGrowSpot = null;
    #nullable disable

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            CheckForClosestSpot();
            if(closestGrowSpot != null)closestGrowSpot.indicator.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            HarvestTree();
        }

        if(INITIAL_SPAWN_COOLDOWN < Time.time)
        {
            CheckTreeStatus();
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
            if(!growSpot.money && (growSpot.cooldown == 0f || Time.timeSinceLevelLoad > growSpot.cooldown))
            {
                growSpot.money = Instantiate(moneyPrefab, growSpot.transform).GetComponent<Money>();
            }
        }
    }

    public void WaterTree()
    {
        foreach (GrowSpot growSpot in spots)
        {
            if (growSpot.money != null)
            {
                growSpot.money.IncreaseTimer(WATER_TIMER_INCREASE);
            }
            else
            {
                growSpot.cooldown -= WATER_TIMER_INCREASE * 1.5f;
            }
        }
    }

    public void HarvestTree()
    {
        if(harvestAllowed == true)
        {
            if(closestGrowSpot != null)
            {
                if(closestGrowSpot.money != null)
                {
                    closestGrowSpot.indicator.SetActive(false);
                    closestGrowSpot.money.StopGrowing();
                    closestGrowSpot.money = null;
                    closestGrowSpot.cooldown = Time.timeSinceLevelLoad + GROW_SPOT_COOLDOWN;
                    closestGrowSpot = null;
                }

                audio.Play();
            }
        }
    }

    private void CheckForClosestSpot()
    {
        Vector3 position = player.transform.position;

        float minDistance = Mathf.Infinity;
        float currentDistance = 0f;
        // Find the growSpot that is closest to the player position
        foreach (GrowSpot growSpot in spots)
        {
            growSpot.indicator.SetActive(false);
            if (growSpot.money != null)
            {
                currentDistance = Vector3.Distance(growSpot.money.transform.position, position);
                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    closestGrowSpot = growSpot;
                }
            }
        }
    }
}
