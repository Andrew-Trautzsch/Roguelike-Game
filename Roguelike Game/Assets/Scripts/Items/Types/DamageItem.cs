using UnityEngine;

[CreateAssetMenu(fileName = "New DamageItem", menuName = "Items/Damage Item")]
public class DamageItem : Items
{
    public override void ApplyEffect(PlayerStats playerStats, int stacks)
    {
        playerStats.playerDamage += 10f * stacks;
        Debug.Log($"Damage increased by {10f * stacks}. Current Damage: {playerStats.playerDamage}");
    }
}
