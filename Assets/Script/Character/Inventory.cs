using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Inventory : MonoBehaviour
{
    private Dictionary<int, int> moneybag = new Dictionary<int, int>();
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
            foreach (KeyValuePair<int, int> kvp in moneybag)
            {
            }
        }
    }
}
