using UnityEngine;

public class EventOnlyInteractable : Interactable
{
    [SerializeField] private Items item; // Reference to the item logic
    [SerializeField] private int stacks = 1; // Number of stacks to apply

    protected override void Interact()
    {
        // Find the PlayerStats component on the Player
        PlayerStats playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

        if (playerStats != null && item != null)
        {
            // Apply the item's effect
            item.ApplyEffect(playerStats, stacks);
            Debug.Log($"{item.GetType().Name} effect applied with {stacks} stack(s).");

            // Destroy the item after interaction
            Destroy(gameObject);
        }
    }
}
