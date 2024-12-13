using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private PlayerStats playerStats;
    private float health;

    [Header("Health Bar")]
    public Image frontHealthBar;
    public Image backHealthBar;
    public float chipSpeed = 2f;

    [Header("Damage Overlay")]
    public Image overlay;
    public float duration;
    public float fadeSpeed;

    private float lerpTimer;
    private float durationTimer;

    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    void Start()
    {
        health = playerStats.maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        UpdateHealthUI();
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, playerStats.maxHealth);
        UpdateHealthUI();

        if (health <= 0)
        {
            // Player dies
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
            else
            {
                Debug.LogError("GameManager instance not found!");
            }
            enabled = false;
            return;
        }

        // Damage overlay fading
        if (overlay.color.a > 0)
        {
            if (health < 30) return;

            durationTimer += Time.deltaTime;
            if (durationTimer >= duration)
            {
                float tmpAlpha = overlay.color.a;
                tmpAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tmpAlpha);
            }
        }
    }

    public void UpdateHealthUI()
    {
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float healthFraction = health / playerStats.maxHealth;

        if (fillBack > healthFraction)
        {
            frontHealthBar.fillAmount = healthFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, healthFraction, lerpTimer / chipSpeed);
        }
        else if (fillBack < healthFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = healthFraction;
            lerpTimer += Time.deltaTime;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, healthFraction, lerpTimer / chipSpeed);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0f;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        health = Mathf.Clamp(health, 0, playerStats.maxHealth);
        lerpTimer = 0f;
        UpdateHealthUI();
    }

    public void HealToFull()
    {
        health = playerStats.maxHealth;
        UpdateHealthUI();
    }
}
