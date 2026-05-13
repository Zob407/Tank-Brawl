using UnityEngine;
using System.Collections;

public class TankShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Shooting")]
    public float bulletSpeed = 20f;
    public float fireRate = 0.5f;
    public float bulletDamage = 25f;

    private float normalFireRate;
    private float normalBulletDamage;
    private float nextFireTime;

    void Start()
    {
        normalFireRate = fireRate;
        normalBulletDamage = bulletDamage;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        Vector3 spawnPosition = firePoint.position + firePoint.forward * 1.5f;

        GameObject bullet = Instantiate(
            bulletPrefab,
            spawnPosition,
            firePoint.rotation
        );

        bullet.name = "PlayerBullet";

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.speed = bulletSpeed;
            bulletScript.damage = bulletDamage;
            bulletScript.SetOwner(gameObject);
        }
    }

    public void FasterShooting(float newFireRate, float duration)
    {
        StopCoroutine(nameof(FireRatePowerUpRoutine));
        StartCoroutine(FireRatePowerUpRoutine(newFireRate, duration));
    }

    IEnumerator FireRatePowerUpRoutine(float newFireRate, float duration)
    {
        fireRate = newFireRate;

        Debug.Log("Fire rate power-up active!");

        yield return new WaitForSeconds(duration);

        fireRate = normalFireRate;

        Debug.Log("Fire rate power-up ended.");
    }

    public void IncreaseDamage(float newDamage, float duration)
    {
        StopCoroutine(nameof(DamagePowerUpRoutine));
        StartCoroutine(DamagePowerUpRoutine(newDamage, duration));
    }

    IEnumerator DamagePowerUpRoutine(float newDamage, float duration)
    {
        bulletDamage = newDamage;

        Debug.Log("Damage power-up active!");

        yield return new WaitForSeconds(duration);

        bulletDamage = normalBulletDamage;

        Debug.Log("Damage power-up ended.");
    }
}