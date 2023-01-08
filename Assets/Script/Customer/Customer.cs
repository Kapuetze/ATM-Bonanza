using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour
{
    public CustomerController cc;

    /// <summary>
    /// How much money does the customer want
    /// </summary>
    int requestedCash = 0;

    /// <summary>
    /// How much money the customer received
    /// </summary>
    int receivedCash = 0;

    /// <summary>
    /// Time left until the customer leaves
    /// </summary>
    float timer;

    /// <summary>
    /// The money deposit the customer is waiting on
    /// </summary>
    private CustomerController customerController;

    private TMP_Text requestedMoneyText;
    private GameController gameController;

    void Awake()
    {
        requestedMoneyText = transform.Find("Canvas/RequestedMoney").GetComponentInChildren<TMP_Text>();
    }

    void Start()
    {
        // Determine how much money the customer wants
        gameController = GameController.instance;
        requestedCash = Random.Range(gameController.difficulty.minRequestedAmount / 10 * 2, gameController.difficulty.maxRequestedAmount / 10 * 2) * 10 / 2;
        print(requestedCash);
        
        // Stay 30s on default + a dynamic amount for the money requested
        timer = Int32.MaxValue;

        requestedMoneyText.text = requestedCash.ToString();

        // TODO: Spawn animation
    }

    void Update()
    {
        // Customer leaves when the time is up
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Leave();
        }
    }

    /// <summary>
    /// Increase cash 
    /// </summary>
    /// <param name="cash"></param>
    public void TakeCash(int cash)
    {
        receivedCash += cash;
        int cashDifference = requestedCash - receivedCash;
        if(cashDifference > cashDifference)
        {
            requestedMoneyText.text = cashDifference.ToString();

            if (cashDifference <= 0)
            {
                // Punish the player if the received cash exceeds what was requested
                GameController.instance.AddScore(requestedCash);

                // TODO: Play cash sound effect

                Leave();
            }
        }
    }

    /// <summary>
    /// Reference the CustomerController who instantiated this customer
    /// </summary>
    /// <param name="controller"></param>
    public void SetCustomerController(CustomerController controller)
    {
        customerController = controller;
    }

    /// <summary>
    /// Customer leaves without giving points
    /// </summary>
    private void Leave()
    {
        // TODO: Leave animation
        customerController.RemoveCustomer(gameObject);
        GameController.instance.IncrementLeftCustomers();
        Debug.Log("Customer left.");
    }
}
