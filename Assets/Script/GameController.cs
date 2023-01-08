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

    public List<Difficulty> difficulties = new List<Difficulty>();

    public Difficulty difficulty;

    /// <summary>
    /// Total of how many customers are going to come
    /// </summary>
    [SerializeField]
    int maxCustomers;

    /// <summary>
    /// Max game time
    /// </summary>
    [SerializeField]
    int maxTimer;

    /// <summary>
    /// Time after which the game gets harder
    /// </summary>
    [SerializeField]
    int difficultyIncreaseInterval = 150;

    /// <summary>
    /// How many customers have already left
    /// </summary>
    int customersLeft;

    /// <summary>
    /// The current score
    /// </summary>
    int score = 0;

    /// <summary>
    /// How much time is left in the game
    /// </summary>
    float timeLeft;

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

        timeLeft = maxTimer;
    }

    void Start()
    {
        difficulty = difficulties[0];
        StartLevel();
        InvokeRepeating("IncreaseDifficulty", difficultyIncreaseInterval, difficultyIncreaseInterval);
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            EndLevel();
        }

        UIController.instance.SetTimer((int)timeLeft);
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
    /// Ends the level and shows score
    /// </summary>
    public void EndLevel()
    {
        // Game is over
        UIController.instance.ShowEndScreen(score);
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
            EndLevel();
        }
    }

    public void IncreaseDifficulty()
    {
        int currentIndex = difficulties.IndexOf(difficulty);
        if(++currentIndex < difficulties.Count -1)
        {
            difficulty = difficulties[currentIndex];
        }
        else
        {
            CancelInvoke("IncreaseDifficulty");
        }
    }
}
