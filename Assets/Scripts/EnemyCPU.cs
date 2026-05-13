using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float turnSpeed = 120f;
    public float wakeUpRange = 20f;
    public float stoppingDistance = 8f;

    [Header("Shooting")]
    public float shootRange = 15f;
    public float fireRate = 2f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Obstacle Avoidance")]
    public LayerMask obstacleMask;
    public float detectDistance = 5f;

    private Rigidbody rb;
    private float nextFireTime;

    private bool avoiding = false;
    private float avoidTimer = 0f;
    private int turnDirection = 1;

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

        GameObject playerObj = GameObject.Find("PlayerTank");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Enemy sleeps until player is close
        if (distanceToPlayer > wakeUpRange)
        {
            return;
        }

        Vector3 rayOrigin = transform.position + Vector3.up * 0.7f;

        bool obstacleAhead = Physics.Raycast(
            rayOrigin,
            transform.forward,
            detectDistance,
            obstacleMask
        );

        if (obstacleAhead && !avoiding)
        {
            avoiding = true;
            avoidTimer = 1.2f;
            turnDirection = Random.value > 0.5f ? 1 : -1;
        }

        if (avoiding)
        {
            avoidTimer -= Time.fixedDeltaTime;

            Quaternion turnAmount = Quaternion.Euler(
                0f,
                turnDirection * turnSpeed * Time.fixedDeltaTime,
                0f
            );

            rb.MoveRotation(rb.rotation * turnAmount);

            Vector3 moveAmount = transform.forward * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + moveAmount);

            if (avoidTimer <= 0f)
            {
                avoiding = false;
            }
        }
        else
        {
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

        // Enemy only shoots when player is close enough
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

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, wakeUpRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
}