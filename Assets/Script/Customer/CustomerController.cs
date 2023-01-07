using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private List<GameObject> customerPrefabs = new List<GameObject>();

    /// <summary>
    /// Point where the customer spawns
    /// </summary>
    [SerializeField]
    private Transform spawnPoint;

    /// <summary>
    /// All available money deposits with customers
    /// </summary>
    [SerializeField]
    private GameObject[] moneyDispensers;

    /// <summary>
    /// The currently waiting customers
    /// </summary>
    private List<GameObject> currentCustomers = new List<GameObject>();

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
    }

    void Start()
    {

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
        GameObject dispenser = GetFreeDispenser();
        if (dispenser != null)
        {
            GameObject newCustomer = Instantiate(GetRandomCustomerPRefab(), spawnPoint.position, Quaternion.identity);
            currentCustomers.Add(newCustomer);
            dispenser.GetComponent<MoneyDispenser>().customer = newCustomer;
        }
    }

    /// <summary>
    /// Get a dispenser without an active customer
    /// </summary>
    /// <returns></returns>
    private GameObject GetFreeDispenser()
    {
        return moneyDispensers.FirstOrDefault(i => i.GetComponent<MoneyDispenser>().customer == null);
    }

    /// <summary>
    /// Removes one customer from the waiting customers
    /// </summary>
    /// <param name="customer"></param>
    public void RemoveCustomer(GameObject customer)
    {
        // Remove customer from active deposit
        moneyDispensers.FirstOrDefault(i => i.GetComponent<MoneyDispenser>().customer == customer).GetComponent<MoneyDispenser>().customer = null;
        currentCustomers.Remove(customer);

        // Spawn a new customer if necessary
        SpawnCustomer();
    }

    private GameObject GetRandomCustomerPRefab()
    {
        int rand = Random.Range(0, customerPrefabs.Count);
        return customerPrefabs[rand];
    }
}
