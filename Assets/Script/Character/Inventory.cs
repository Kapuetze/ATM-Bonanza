using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Inventory : MonoBehaviour
{
    private Dictionary<Denomination, int> moneybag = new Dictionary<Denomination, int>();
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
            foreach (Denomination d in moneybag.Keys)
            {
                if(d == other.denomination)
                {
                    moneybag[d] += 1; 
                }
            }
        }
    }
}
