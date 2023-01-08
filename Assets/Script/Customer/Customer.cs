using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class Customer : MonoBehaviour
{
    public CustomerController cc;
    public float speed = 2.0f;
    /// <summary>
    /// How much extra time ist added to the time left. Servicing more customers will lead to a longer play time
    /// </summary>
    public float gameTimeAdditon = 3f;

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
    private Tween animation;
    private bool hasWalkedIn = false;
    private AudioSource audio;

    [SerializeField]
    private AudioClip successSound;
    [SerializeField]
    private AudioClip errorSound;
    [SerializeField]
    private AudioClip finishSound;
    [SerializeField]
    private AudioClip needMoneySound;

    void Awake()
    {
        requestedMoneyText = transform.Find("Canvas/RequestedMoney").GetComponentInChildren<TMP_Text>();

        audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        // Determine how much money the customer wants
        gameController = GameController.instance;
        requestedCash = Random.Range(gameController.difficulty.minRequestedAmount * 2 / 10, gameController.difficulty.maxRequestedAmount * 2 / 10) * 10 / 2;
        print(requestedCash);

        requestedMoneyText.text = requestedCash.ToString();

        // TODO: Spawn animation
    }

    void Update()
    {
        // Customer leaves when the time is up
        timer -= Time.deltaTime;
        if (hasWalkedIn == true && timer <= 0)
        {
            StartLeaveAnimation();
        }
    }

    /// <summary>
    /// Increase cash 
    /// </summary>
    /// <param name="cash"></param>
    public bool TakeCash(int cash)
    {
        int cashDifference = requestedCash - receivedCash;
        if (cashDifference >= cash)
        {
            receivedCash += cash;
            cashDifference -= cash;
            requestedMoneyText.text = cashDifference.ToString();

            if (cashDifference <= 0)
            {
                // Punish the player if the received cash exceeds what was requested
                GameController.instance.AddScore(requestedCash);
                GameController.instance.AddToTimeLeft(gameTimeAdditon);
                // TODO: Play cash sound effect
                audio.PlayOneShot(finishSound);

                StartLeaveAnimation();
            }
            else
            {
                audio.PlayOneShot(successSound);
            }

            return true;
        }
        else
        {
            audio.PlayOneShot(errorSound);
            return false;
        }
    }

    /// <summary>
    /// Reference the CustomerController who instantiated this customer
    /// </summary>
    /// <param name="controller"></param>
    public void SetCustomerController(CustomerController controller)
    {
        customerController = controller;

        transform.DOShakeScale(speed, 0.5f, 10, 90, false);
        transform.DOMove(customerController.customerDestinationPoint.position, speed).OnComplete(() =>
        {
            hasWalkedIn = true;
            // For now we disabled automatic timer leave
            timer = Int32.MaxValue;
            audio.PlayOneShot(needMoneySound);
        });
    }


    private void StartLeaveAnimation()
    {
        transform.DOShakeScale(speed, 0.5f, 10, 90, false);
        transform.DOMove(customerController.customerSpawnPoint.position, speed).OnComplete(() =>
        {
            Leave();
        });
    }
    /// <summary>
    /// Customer leaves without giving points
    /// </summary>
    private void Leave()
    {
        customerController.RemoveCustomer(gameObject);
        GameController.instance.IncrementLeftCustomers();
        Debug.Log("Customer left.");
    }
}
