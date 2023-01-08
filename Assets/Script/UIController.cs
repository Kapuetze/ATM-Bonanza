using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    /// <summary>
    /// Singleton instance
    /// </summary>
    public static UIController instance;

    private GameObject mainMenu;
    private GameObject inventory;

    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text timerText;
    [SerializeField]
    private GameObject moneyIconPrefab;

    [SerializeField]
    private GameObject endScreen;
    [SerializeField]
    private TMP_Text endScore;

    private AudioSource audioSource;

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

        mainMenu = transform.Find("MainMenu").gameObject;
        inventory = transform.Find("Inventory").gameObject;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Sets the current score in the UI text field
    /// </summary>
    /// <param name="score"></param>
    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    /// <summary>
    /// Sets the current timer in the UI text field
    /// </summary>
    /// <param name="score"></param>
    public void SetTimer(int timer)
    {
        timerText.text = new TimeSpan(0, 0, timer).ToString("mm\\:ss");
    }

    /// <summary>
    /// Toggle the main menu on or off
    /// </summary>
    public void ToggleMainMenu()
    {
        if (mainMenu.activeInHierarchy)
        {
            mainMenu.SetActive(false);
            GameController.instance.PauseLevel();
        }
        else
        {
            GameController.instance.ResumeLevel();
            mainMenu.SetActive(true);
        }
    }

    /// <summary>
    /// Sets the inventory to display
    /// </summary>
    public void SetInventory(Dictionary<Denomination, int> newInventory)
    {
        // Clear inventory list
        foreach (Transform child in inventory.transform)
        {
            Destroy(child.gameObject);
        }

        // Add new inventory items
        foreach (var item in newInventory)
        {
            // Instantiate new icon
            GameObject newIcon = Instantiate(moneyIconPrefab, inventory.transform);
            newIcon.GetComponent<MoneyIcon>().SetMoneyValues(item.Key, item.Value);
        }
    }

    public void ShowEndScreen(int score)
    {
        endScore.text = score.ToString();
        endScreen.SetActive(true);
        audioSource.Play();
    }

    public void HideEndScreen()
    {
        endScreen.SetActive(false);
    }
}
