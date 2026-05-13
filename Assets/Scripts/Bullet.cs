using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 25f;
    public float speed = 20f;
    public float lifeTime = 3f;

    private GameObject owner;

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
        Collider col = GetComponent<Collider>();

        if (col != null)
        {
            col.isTrigger = true;
        }

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
        Debug.Log("Bullet touched: " + other.gameObject.name);

        if (owner != null)
        {
            if (other.gameObject == owner || other.transform.IsChildOf(owner.transform))
            {
                Debug.Log("Ignored owner: " + other.gameObject.name);
                return;
            }
        }

        Health health = other.GetComponent<Health>();

        if (health == null)
        {
            health = other.GetComponentInParent<Health>();
        }

        if (health != null)
        {
            Debug.Log("Damaging: " + health.gameObject.name);
            health.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        Debug.Log("No Health found on: " + other.gameObject.name);
        Destroy(gameObject);
    }
}