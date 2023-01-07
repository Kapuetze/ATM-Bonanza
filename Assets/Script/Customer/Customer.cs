using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Customer : MonoBehaviour
{
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
    /// Cash notes possible
    /// </summary>
    [SerializeField]
    private int[] possibleCash;

    private TMP_Text requestedMoneyText;

    void Awake()
    {
        requestedMoneyText = transform.Find("Canvas/RequestedMoney").GetComponentInChildren<TMP_Text>();
    }

    void Start()
    {
        // Determine how much money the customer wants
        requestedCash = possibleCash[Random.Range(0, possibleCash.Length)];
        timer = requestedCash;

        requestedMoneyText.text = requestedCash.ToString();
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
        if (cashDifference <= 0)
        {
            // Punish the player if the received cash exceeds what was requested
            GameController.instance.AddScore(requestedCash + cashDifference);

            // TODO: Play cash sound effect

            Leave();
        }
    }

    /// <summary>
    /// Customer leaves without giving points
    /// </summary>
    private void Leave()
    {
        CustomerController.instance.RemoveCustomer(gameObject);
        Destroy(gameObject);
        Debug.Log("Customer left.");
    }
}
