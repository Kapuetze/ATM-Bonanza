using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Inventory : MonoBehaviour
{
    private Dictionary<Denomination, int> moneyBag = new Dictionary<Denomination, int>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            moneyBag[denomination] += 1;
        }

        UIController.instance.SetInventory(moneyBag);
    }
}
