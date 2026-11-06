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
    [SerializeField] private GameObject GameOverScreen;

    Rigidbody rb;
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.OnDeath += Die;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(moveInput.x, 0, moveInput.y) * Time.deltaTime * speed;

        Vector3 mousePos = Mouse.current.position.ReadValue();

        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);

        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float hitDistaance;
        if(groundPlane.Raycast(mouseRay, out hitDistaance))
        {
            Vector3 hitPos = mouseRay.GetPoint(hitDistaance);

            Vector3 lookDirection = hitPos - transform.position;
            lookDirection.y = 0.0f;

            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        //Debug.Log($"Moving {moveInput}");
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

    void Die()
    {
        GameOverScreen.SetActive(true);

        if (health != null)
            health.OnDeath -= Die;
        Time.timeScale = 0;
    }
}
