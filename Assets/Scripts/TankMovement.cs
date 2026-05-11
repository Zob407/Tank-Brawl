using UnityEngine;
//Test123
public class TankMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 120f;

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
        rb.position = new Vector3(rb.position.x, 0f, rb.position.z);
    }

    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
        Debug.Log("Move: " + moveInput + " Turn: " + turnInput);
    }

    void FixedUpdate()
    {
        Vector3 moveAmount = transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime;
        moveAmount.y = 0f; // stops vertical movement
        rb.MovePosition(rb.position + moveAmount);

        Quaternion turnAmount = Quaternion.Euler(0f, turnInput * turnSpeed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * turnAmount);
    }
}