using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType
    {
        Health,
        FasterShooting,
        MoreDamage
    }

    public PowerUpType powerUpType;

    [Header("Health Power-Up")]
    public float healAmount = 50f;

    [Header("Shooting Power-Up")]
    public float fasterFireRate = 0.2f;
    public float increasedDamage = 50f;
    public float duration = 8f;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (powerUpType == PowerUpType.Health)
        {
            Health health = other.GetComponent<Health>();

            if (health != null)
            {
                health.Heal(healAmount);
            }
        }

        if (powerUpType == PowerUpType.FasterShooting)
        {
            TankShoot shoot = other.GetComponent<TankShoot>();

            if (shoot != null)
            {
                shoot.FasterShooting(fasterFireRate, duration);
            }
        }

        if (powerUpType == PowerUpType.MoreDamage)
        {
            TankShoot shoot = other.GetComponent<TankShoot>();

            if (shoot != null)
            {
                shoot.IncreaseDamage(increasedDamage, duration);
            }
        }

        Destroy(gameObject);
    }
}