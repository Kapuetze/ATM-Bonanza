using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Denomination { Five, Ten, Twenty, Fifty, Onehundred, Twohundred, Fivehundred}
public class Bill
{
    private Denomination denomination;
    private int value = 5;

    public Bill() 
    {
        SetValue(Denomination.Five);
    }

    public Bill(Denomination deno)
    {
        SetValue(deno);
    }

    public int GetValue()
    {
        return value;
    }

    public Denomination GetValueAsDenomination()
    {
        return denomination;
    }

    private void SetValue(Denomination deno)
    {
        denomination = deno;
        switch(deno)
        {
            case Denomination.Five:
                value = 5;
                break;
            case Denomination.Ten:
                value = 10;
                break;
            case Denomination.Twenty:
                value = 20;
                break;
            case Denomination.Fifty:
                value = 50;
                break;
            case Denomination.Onehundred:
                value = 100;
                break;
            case Denomination.Twohundred:
                value = 200;
                break;
            case Denomination.Fivehundred:
                value = 500;
                break;
        }
    }

}

public class Money : MonoBehaviour
{
    public Bill bill = new Bill();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
