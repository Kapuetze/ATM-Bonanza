using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    /// <summary>
    /// The customer gameobject
    /// </summary>
    [SerializeField]
    private List<GameObject> customerPrefabs = new List<GameObject>();

    /// <summary>
    /// Point where the customer spawns
    /// </summary>
    [SerializeField]
    private Transform customerSpawnPoint;

    /// <summary>
    /// The currently waiting customers
    /// </summary>
    [HideInInspector]
    public Customer currentCustomer;

    void Start()
    {
        SpawnCustomer();
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
        currentCustomer = Instantiate(GetRandomCustomerPrefab(), customerSpawnPoint.position, Quaternion.identity).GetComponent<Customer>();
        currentCustomer.SetCustomerController(this); 
    }

    /// <summary>
    /// Removes one customer from the waiting customers
    /// </summary>
    /// <param name="customer"></param>
    public void RemoveCustomer(GameObject customer)
    {
        Destroy(currentCustomer.gameObject);
        // Spawn a new customer if necessary
        SpawnCustomer();
    }

    private GameObject GetRandomCustomerPrefab()
    {
        int rand = Random.Range(0, customerPrefabs.Count);
        return customerPrefabs[rand];
    }
}
