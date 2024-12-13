using UnityEngine;

[CreateAssetMenu(fileName = "New SpeedItem", menuName = "Items/Speed Item")]
public class SpeedItem : Items
{
    public override void ApplyEffect(PlayerStats playerStats, int stacks)
    {
        playerStats.playerSpeed += 1.5f * stacks;
        Debug.Log($"Speed increased by {1.5f * stacks}. Current Speed: {playerStats.playerSpeed}");
    }
}
