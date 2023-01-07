using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// Singleton instance
    /// </summary>
    public static GameController instance;

    /// <summary>
    /// Total of how many customers are going to come
    /// </summary>
    [SerializeField]
    int maxCustomers;

    /// <summary>
    /// How many customers have already left
    /// </summary>
    int customersLeft;

    /// <summary>
    /// The current score
    /// </summary>
    int score = 0;

    // Start is called before the first frame update
    void Awake()
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
    }

    void Start()
    {
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
    }

    /// <summary>
    /// Restarts the scene
    /// </summary>
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Add score to the game score
    /// </summary>
    /// <param name="amount">The score amount to be added</param>
    public void AddScore(int amount)
    {
        score += amount;

        UIController.instance.SetScore(score);
    }

    public void IncrementLeftCustomers()
    {
        customersLeft++;
        if (customersLeft == maxCustomers)
        {
            // Game is over

        }
    }
}
