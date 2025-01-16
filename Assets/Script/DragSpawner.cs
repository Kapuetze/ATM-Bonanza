using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragSpawner : MonoBehaviour
{
    public GameObject prefab;

    public void Spawn()
    {
        Debug.Log("Spawn");
        GameObject newSpawn = Instantiate(prefab, transform.position, prefab.transform.rotation);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Spawn();
        }
    }

    void OnMouseDown()
    {
        Spawn();
    }
}
