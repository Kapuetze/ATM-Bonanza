using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItem : MonoBehaviour
{
    public GameObject prefab;
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            DropItem();
            Destroy(gameObject);
        }
    }

    void DropItem()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
