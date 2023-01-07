using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLogic : MonoBehaviour
{
    private const float SPAWN_INTERVAL = 0.3f;
    private const int GROW_SPOT_COUNT = 3;
    private const float GROW_SPOT_COOLDOWN = 10.0f;
    public List<GrowSpot> spots = new List<GrowSpot>();

    [SerializeField]
    private GameObject moneyPrefab;

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
        foreach(GrowSpot growSpot in spots)
        {
            // Check if grow spot is empty and cooldown exceeded
            if(!growSpot.money && (growSpot.cooldown == 0f || Time.time > growSpot.cooldown))
            {
                growSpot.money = Instantiate(moneyPrefab, growSpot.transform);
            }
        }
    }

    public void HarvestTree(Vector3 position)
    {
        // Find the growSpot that is closest to the player position
        foreach (GrowSpot growSpot in spots)
        {
            
        }
    }
}
