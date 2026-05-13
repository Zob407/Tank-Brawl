using UnityEngine;

public class TankShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float bulletSpeed = 20f;
    public float fireRate = 0.5f;

    private float nextFireTime;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        // Spawn bullet slightly in front of FirePoint so it does not touch player
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
            bulletScript.SetOwner(gameObject);
        }
    }
}