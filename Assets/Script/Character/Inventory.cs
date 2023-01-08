using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Inventory : MonoBehaviour
{
    MoneyDispenser currentDispenserZone = null;

    int currentItem = -1;

    private Dictionary<Denomination, int> moneyBag = new Dictionary<Denomination, int>();
    [SerializeField]
    private GameObject selectedMoneyIcon;
    [SerializeField]
    private AudioClip takeSound;

    private AudioSource audio;

    // Start is called before the first frame update
    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta != Vector2.zero)
        {            
            SwitchCurrentItem((int)Input.mouseScrollDelta.y);
        }

        //Interact with F
        if (Input.GetKeyDown(KeyCode.E) && GetCurrentSelectedItem() != null && currentDispenserZone != null && currentDispenserZone.customerController.currentCustomer != null)
        {
            Item item = GetCurrentSelectedItem();
            bool success = currentDispenserZone.customerController.currentCustomer.TakeCash((int)item.denomination);

            if(success == true)
            {
                RemoveBill(item.denomination);
            }
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

            audio.PlayOneShot(takeSound);
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

        Item item = GetCurrentSelectedItem();
        if (item != null)
        {
            // Display currently selected item in the UI
            SetSelectedItem(item);
        }
        else
        {
            RemoveSelectedItem();
        }
    }

    private Item GetCurrentSelectedItem()
    {
        return GetItemAt(currentItem);
    }

    /// <summary>
    /// Get the currently selected denomination or null if nothing is selected
    /// </summary>
    /// <returns></returns>
    private Item GetItemAt(int index)
    {
        Item result = null;
        if (moneyBag.Count > 0 && index != -1)
        {
            var element = moneyBag.ElementAt(index);
            result = new Item
            {
                denomination = element.Key,
                amount = element.Value
            };
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

        if (moneyBag.Count == 1)
        {
            currentItem = 0;

            Item item = GetItemAt(0);
            SetSelectedItem(item);
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

            // Update the selected item UI
            SetSelectedItem(new Item { denomination = denomination, amount = moneyBag[denomination] });

            // Remove the whole key entry if it is 0
            if (moneyBag[denomination] == 0)
            {
                RemoveSelectedItem();
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
    void SetSelectedItem(Item item)
    {
        if (!selectedMoneyIcon.activeInHierarchy)
            selectedMoneyIcon.SetActive(true);

        selectedMoneyIcon.GetComponent<MoneyIcon>().SetMoneyValues(item.denomination, item.amount);
    }

    /// <summary>
    /// Set the currently selected item in the UI
    /// </summary>
    /// <param name="currentDenomination"></param>
    /// <exception cref="NotImplementedException"></exception>
    void RemoveSelectedItem()
    {
        currentItem = 0;
        selectedMoneyIcon.SetActive(false);
    }
}

class Item
{
    public Denomination denomination { get; set; }
    public int amount { get; set; }
}