using UnityEngine;

public abstract class Items : ScriptableObject
{
    public abstract void ApplyEffect(PlayerStats playerStats, int stacks);
}
