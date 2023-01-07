using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

[RequireComponent(typeof(Collider2D))]
public class Inventory : MonoBehaviour
{
    MoneyDispenser currentDispenserZone = null;
    int currentItem = 0;

    private Dictionary<Denomination, int> moneyBag = new Dictionary<Denomination, int>();

    [SerializeField]
    private GameObject selectedMoneyIcon;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta != Vector2.zero)
        {            
            SwitchCurrentItem((int)Input.mouseScrollDelta.y);
        }

        //Interact with F
        if (Input.GetKeyDown(KeyCode.F) && GetCurrentSelectedItem() != null && currentDispenserZone != null)
        {
            Denomination denomination = GetCurrentSelectedItem().Value;
            currentDispenserZone.customer.GetComponent<Customer>().TakeCash((int)denomination);

            RemoveBill(denomination);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Money other;
        if(collision.collider.gameObject.TryGetComponent<Money>(out other))
        {
            // Add the money to current inventory
            AddBill(other.denomination);
            // Destroy the money object
            Destroy(collision.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<MoneyDispenser>(out MoneyDispenser dispenser))
        {
            currentDispenserZone = dispenser;
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<MoneyDispenser>(out MoneyDispenser dispenser))
        {
            currentDispenserZone = null;
        }
    }

    /// <summary>
    /// Switch the currently selected item
    /// </summary>
    /// <param name="direction"></param>
    private void SwitchCurrentItem(int direction)
    {
        currentItem += direction;
        currentItem = Mathf.Clamp(currentItem, 0, moneyBag.Count - 1);
        if (moneyBag.Count > 0)
        {
            var element = moneyBag.ElementAt(currentItem);
            // Display currently selected item in the UI
            SetSelectedItem(element.Key, element.Value);
        }
        else
        {
            RemoveSelectedItem();
        }
    }

    /// <summary>
    /// Get the currently selected denomination or null if nothing is selected
    /// </summary>
    /// <returns></returns>
    private Denomination? GetCurrentSelectedItem()
    {
        Denomination? result = null;
        if (moneyBag.Count > 0)
        {
            result = moneyBag.ElementAt(currentItem).Key;
        }

        return result;
    }

    /// <summary>
    /// Add a bill to current inventory
    /// </summary>
    /// <param name="denomination"></param>
    public void AddBill(Denomination denomination)
    {
        if (moneyBag.ContainsKey(denomination))
        {
            moneyBag[denomination] += 1;
        }
        else
        {
            moneyBag.Add(denomination, 1);
        }

        UIController.instance.SetInventory(moneyBag);
    }

    /// <summary>
    /// Remove a single bill from the inventory
    /// </summary>
    /// <param name="denomination"></param>
    public void RemoveBill(Denomination denomination)
    {
        if (moneyBag.ContainsKey(denomination))
        {
            moneyBag[denomination] -= 1;

            // Remove the whole key entry if it is 0
            if (moneyBag[denomination] == 0)
            {
                moneyBag.Remove(denomination);
            }
        }

        UIController.instance.SetInventory(moneyBag);
    }

    /// <summary>
    /// Set the currently selected item in the UI
    /// </summary>
    /// <param name="currentDenomination"></param>
    /// <exception cref="NotImplementedException"></exception>
    void SetSelectedItem(Denomination currentDenomination, int amount)
    {
        if (!selectedMoneyIcon.activeInHierarchy)
            selectedMoneyIcon.SetActive(true);

        // Get the color from enumeration description
        string hexColor = currentDenomination.GetDescription();
        ColorUtility.TryParseHtmlString(hexColor, out Color color);
        selectedMoneyIcon.GetComponent<Image>().color = color;

        // Change the text to the current amount
        selectedMoneyIcon.transform.Find("MoneyText").GetComponent<TMP_Text>().text = amount.ToString();
    }

    /// <summary>
    /// Set the currently selected item in the UI
    /// </summary>
    /// <param name="currentDenomination"></param>
    /// <exception cref="NotImplementedException"></exception>
    void RemoveSelectedItem()
    {
        selectedMoneyIcon.SetActive(false);
    }
}
