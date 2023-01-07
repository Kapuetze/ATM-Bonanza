using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// Singleton instance
    /// </summary>
    public static GameController instance;

    /// <summary>
    /// The current score
    /// </summary>
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Create singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Starts the current level
    /// </summary>
    public void StartLevel()
    {
        // Spawn the first customer
        CustomerController.instance.SpawnCustomer();
    }

    /// <summary>
    /// Add score to the game score
    /// </summary>
    /// <param name="amount">The score amount to be added</param>
    public void AddScore(int amount)
    {
        score += amount;
    }
}
