using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    /// <summary>
    /// Singleton instance
    /// </summary>
    public static UIController instance;

    private TMP_Text scoreText;
    private GameObject mainMenu;

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
        scoreText = transform.Find("Score").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    /// Toggle the main menu on or off
    /// </summary>
    public void ToggleMainMenu()
    {
        if (mainMenu.activeInHierarchy)
        {
            mainMenu.SetActive(false);
        }
        else
        {
            mainMenu.SetActive(true);
        }
    }
}
