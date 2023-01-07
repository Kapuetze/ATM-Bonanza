using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Inventory : MonoBehaviour
{
    MoneyDeposit currentDepositZone = null;
    int currentItem = 0;

    private Dictionary<Denomination, int> moneyBag = new Dictionary<Denomination, int>();

    private GameObject selectedItem;

    // Start is called before the first frame update
    void Awake()
    {
        selectedItem = transform.Find("MoneyIcon").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta != Vector2.zero)
        {            
            SwitchCurrentItem((int)Input.mouseScrollDelta.y);
        }

        // Interact with F
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    currentDepositZone.Customer
        //}
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
        MoneyDeposit deposit;
        if (collider.gameObject.TryGetComponent<MoneyDeposit>(out deposit))
        {
            currentDepositZone = deposit;
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        MoneyDeposit deposit;
        if (collider.gameObject.TryGetComponent<MoneyDeposit>(out deposit))
        {
            currentDepositZone = null;
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
            Denomination current = moneyBag.ElementAt(currentItem).Key;
            // Display currently selected item in the UI
            SetSelectedItem(current);
        }
        else
        {
            RemoveSelectedItem();
        }
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
    void SetSelectedItem(Denomination currentDenomination)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Set the currently selected item in the UI
    /// </summary>
    /// <param name="currentDenomination"></param>
    /// <exception cref="NotImplementedException"></exception>
    void RemoveSelectedItem()
    {
        throw new NotImplementedException();
    }
}
