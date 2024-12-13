using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private float damage; // The damage this bullet will deal

    // Called during bullet instantiation to set its damage
    public void Initialize(float playerDamage)
    {
        damage = playerDamage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Transform hitTransform = collision.transform;

        // Check if the object hit is tagged as an enemy
        if (hitTransform.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");

            // Try to get the Enemy component and apply damage
            Enemy enemy = hitTransform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Apply the damage
                Debug.Log($"Dealt {damage} damage to {hitTransform.name}. Remaining Health: {enemy.health}");
            }
        }

        // Destroy the bullet on collision
        Destroy(gameObject);
    }
}
