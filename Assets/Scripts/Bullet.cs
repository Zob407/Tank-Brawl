using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 25f;
    public float speed = 20f;
    public float lifeTime = 3f;

    private GameObject owner;
    private bool alreadyHit = false;

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;

        Collider bulletCollider = GetComponent<Collider>();

        if (owner != null && bulletCollider != null)
        {
            Collider[] ownerColliders = owner.GetComponentsInChildren<Collider>();

            foreach (Collider ownerCollider in ownerColliders)
            {
                Physics.IgnoreCollision(bulletCollider, ownerCollider, true);
            }
        }
    }

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = false;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rb.linearVelocity = transform.forward * speed;
        }

        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        TryHit(other.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        TryHit(collision.gameObject);
    }

    void TryHit(GameObject hitObject)
    {
        if (alreadyHit) return;

        if (owner != null)
        {
            if (hitObject == owner)
            {
                return;
            }

            if (hitObject.transform.IsChildOf(owner.transform))
            {
                return;
            }
        }

        Health health = hitObject.GetComponent<Health>();

        if (health == null)
        {
            health = hitObject.GetComponentInParent<Health>();
        }

        if (health != null)
        {
            alreadyHit = true;
            health.TakeDamage(damage);
            Debug.Log("Bullet damaged: " + health.gameObject.name);
            Destroy(gameObject);
            return;
        }

        alreadyHit = true;
        Destroy(gameObject);
    }
}