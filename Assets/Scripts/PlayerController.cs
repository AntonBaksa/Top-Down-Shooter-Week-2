using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveInput;

    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float jumpCheckRadius = 1.0f;
    [SerializeField] private Vector3 offset = new Vector3(0.0f, -0.5f, 0.0f);

    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(moveInput.x, 0, moveInput.y) * Time.deltaTime * speed);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log($"Moving {moveInput}");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && rb)
        {
            if (IsGrounded())
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
            }
        }    
    }

    private bool IsGrounded()
    {
        Collider[] ground = Physics.OverlapSphere(transform.position + offset, jumpCheckRadius, LayerMask.GetMask("Ground"));

        return ground.Length > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + offset, jumpCheckRadius);
    }
}
