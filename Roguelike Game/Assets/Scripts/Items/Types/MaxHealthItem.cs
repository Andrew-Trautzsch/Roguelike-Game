using UnityEngine;

[CreateAssetMenu(fileName = "New MaxHealthItem", menuName = "Items/Max Health Item")]
public class MaxHealthItem : Items
{
    public override void ApplyEffect(PlayerStats playerStats, int stacks)
    {
        playerStats.maxHealth += 10f * stacks; // Increase max health
        PlayerHealth playerHealth = playerStats.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.HealToFull(); // Fully heal the player
        }

        Debug.Log($"Max Health increased by {10f * stacks}. Current Max Health: {playerStats.maxHealth}");
    }
}
