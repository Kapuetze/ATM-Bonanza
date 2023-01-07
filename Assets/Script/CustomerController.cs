using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    /// <summary>
    /// Singleton instance
    /// </summary>
    public static CustomerController instance;

    /// <summary>
    /// The customer gameobject
    /// </summary>
    [SerializeField]
    private GameObject customerPrefab;

    /// <summary>
    /// The currently waiting customers
    /// </summary>
    private List<GameObject> currentCustomers = new List<GameObject>();


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
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Spawn a single new customer
    /// </summary>
    public void SpawnCustomer()
    {
        currentCustomers.Add(Instantiate(customerPrefab, Vector2.one, Quaternion.identity));
    }

    /// <summary>
    /// Removes one customer from the waiting customers
    /// </summary>
    /// <param name="customer"></param>
    public void RemoveCustomer(GameObject customer)
    {
        currentCustomers.Remove(customer);

        // Spawn a new customer if necessary
        SpawnCustomer();
    }
}
