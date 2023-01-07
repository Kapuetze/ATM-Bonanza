using System.Collections;
using System.Collections.Generic;
using UnityEngine;

private GrowSpot {
    GameObject gameObject;
    Position position;
    float cooldown = 0f;
}

public class TreeLogic : MonoBehaviour
{
    private const float SPAWN_INTERVAL = 0.3f;
    private const int GROW_SPOT_COUNT = 3;
    private const float GROW_SPOT_COOLDOWN = 10.0f;

    [SerializeField]
    private GameObject moneyPrefab;

    private GrowSpot spot1 = new GrowSpot();
    private GrowSpot spot2 = new GrowSpot();
    private GrowSpot spot3 = new GrowSpot();

    private GrowSpot[] growSpots = new GrowSpot[] {spot1, spot2, spot3};

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckTreeStatus", SPAWN_INTERVAL, SPAWN_INTERVAL);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CheckTreeStatus()
    {
        foreach(GrowSpot growSpot in growSpots)
        {
            // Check if grow spot is empty and cooldown exceeded
            if(!growSpot.gameObject && (growSpot.cooldown === 0f || Time.time > growSpot.cooldown))
            {
                growSpot.gameObject = Instantiate(moneyPrefab);
            }
        }
    }

    public void HarvestTree(Position position)
    {
        // Find the growSpot that is closest to the player position
        foreach (GrowSpot growSpot in collection)
        {
            
        }
    }
}
