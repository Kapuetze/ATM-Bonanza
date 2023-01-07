using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class UIController : MonoBehaviour
{
    /// <summary>
    /// Singleton instance
    /// </summary>
    public static UIController instance;

    private TMP_Text scoreText;
    private GameObject mainMenu;
    private GameObject inventory;

    [SerializeField]
    private GameObject moneyIconPrefab;

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

            // Get the color from enumeration description
            string hexColor = item.Key.GetDescription();
            ColorUtility.TryParseHtmlString(hexColor, out Color color);
            newIcon.GetComponent<Image>().color = color;

            // Change the text to the current amount
            newIcon.transform.Find("MoneyText").GetComponent<TMP_Text>().text = item.Value.ToString();
        }
    }
}
