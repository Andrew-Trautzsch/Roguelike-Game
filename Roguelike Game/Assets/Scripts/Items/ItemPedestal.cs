using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPedestal : MonoBehaviour
{
    public GameObject[] items;
    public Transform itemSpawnPoint;

    void Start()
    {
        SpawnRandomItem();
    }

    void SpawnRandomItem()
    {
        if (items.Length > 0)
        {
            int randomIndex = Random.Range(0, items.Length);
            Instantiate(items[randomIndex], itemSpawnPoint.position, Quaternion.identity, transform);
        }
    }
}
