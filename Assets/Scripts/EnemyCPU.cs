using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float turnSpeed = 80f;
    public float shootRange = 15f;
    public float fireRate = 2f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Rigidbody rb;
    private float nextFireTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | 
                         RigidbodyConstraints.FreezeRotationZ | 
                         RigidbodyConstraints.FreezePositionY;
        if (GameObject.Find("PlayerTank") != null)
        {
            player = GameObject.Find("PlayerTank").transform;
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // Rotate toward player
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime));

        // Move toward player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > 8f)
        {
            Vector3 moveAmount = transform.forward * moveSpeed * Time.fixedDeltaTime;
            moveAmount.y = 0f;
            rb.MovePosition(rb.position + moveAmount);
        }

        // Shoot at player
        if (distanceToPlayer <= shootRange && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.name = "EnemyBullet";
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.linearVelocity = firePoint.forward * 20f;
        Destroy(bullet, 3f);
    }
}