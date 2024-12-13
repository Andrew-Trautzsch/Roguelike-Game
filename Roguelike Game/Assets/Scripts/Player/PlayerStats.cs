using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public List<ItemList> items = new List<ItemList>();

    public float maxHealth = 100;
    public float playerDamage = 15;
    public float playerSpeed = 5;

    private float baseMaxHealth = 100;
    private float basePlayerDamage = 10;
    private float basePlayerSpeed = 5;

    void Start()
    {
        StartCoroutine(CallItemUpdate());
    }

    public void UpdateStats()
    {
        // Reset stats to base values
        maxHealth = baseMaxHealth;
        playerDamage = basePlayerDamage;
        playerSpeed = basePlayerSpeed;

        // Apply all item effects
        foreach (ItemList item in items)
        {
            item.item.ApplyEffect(this, item.stacks);
        }

        // Attempt to heal the player to full if PlayerHealth is present
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.HealToFull(); // Fully heal if max health changes
        }
        else
        {
            Debug.LogWarning("PlayerHealth component is missing on the player!");
        }
    }

    IEnumerator CallItemUpdate()
    {
        while (true)
        {
            UpdateStats();
            yield return new WaitForSeconds(0.5f); // Periodic updates
        }
    }

    public void AddItem(Items newItem, string itemName, int stacks)
    {
        // Check if the item already exists
        ItemList existingItem = items.Find(i => i.name == itemName);
        if (existingItem != null)
        {
            existingItem.stacks += stacks;
        }
        else
        {
            items.Add(new ItemList(newItem, itemName, stacks));
        }

        // Update stats immediately
        UpdateStats();
    }
}