using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public float moveSpeed = 3f;
    public float turnSpeed = 120f;
    public float shootRange = 15f;
    public float stoppingDistance = 8f;
    public float fireRate = 2f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    public LayerMask obstacleMask;
    public float detectDistance = 5f;

    private Rigidbody rb;
    private float nextFireTime;

    private bool avoiding = false;
    private float avoidTimer = 0f;
    private int turnDirection = 1; // 1 = right, -1 = left

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = true;
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

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

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        Vector3 rayOrigin = transform.position + Vector3.up * 0.7f;

        bool obstacleAhead = Physics.Raycast(
            rayOrigin,
            transform.forward,
            detectDistance,
            obstacleMask
        );

        // If enemy sees wall, start avoiding
        if (obstacleAhead && !avoiding)
        {
            avoiding = true;
            avoidTimer = 1.2f;

            // Randomly choose left or right
            turnDirection = Random.value > 0.5f ? 1 : -1;
        }

        if (avoiding)
        {
            avoidTimer -= Time.fixedDeltaTime;

            // Turn left/right
            Quaternion turnAmount = Quaternion.Euler(
                0f,
                turnDirection * turnSpeed * Time.fixedDeltaTime,
                0f
            );

            rb.MoveRotation(rb.rotation * turnAmount);

            // Move forward while avoiding
            Vector3 moveAmount = transform.forward * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + moveAmount);

            // Stop avoiding after timer finishes
            if (avoidTimer <= 0f)
            {
                avoiding = false;
            }
        }
        else
        {
            // Normal chase player
            Vector3 direction = player.position - transform.position;
            direction.y = 0f;
            direction.Normalize();

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                rb.MoveRotation(
                    Quaternion.RotateTowards(
                        rb.rotation,
                        targetRotation,
                        turnSpeed * Time.fixedDeltaTime
                    )
                );
            }

            if (distanceToPlayer > stoppingDistance)
            {
                Vector3 moveAmount = transform.forward * moveSpeed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + moveAmount);
            }
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

        if (bulletRb != null)
        {
            bulletRb.linearVelocity = firePoint.forward * 20f;
        }

        Destroy(bullet, 3f);
    }

    void OnDrawGizmosSelected()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.7f;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(rayOrigin, transform.forward * detectDistance);
    }
}