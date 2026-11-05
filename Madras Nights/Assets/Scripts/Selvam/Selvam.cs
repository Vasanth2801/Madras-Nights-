using UnityEngine;
using UnityEngine.InputSystem;

public class Selvam : MonoBehaviour
{
    Playercontrols controls;
    Rigidbody2D rb;
    Vector2 move;
    public float speed = 5f;
    public float jumpForce = 10f;

    public float jumpGravity = 1f;
    public float fallGravity = 2.5f;
    public float normalGravity = 3f;

    private void Awake()
    {
        controls = new Playercontrols();
        rb = GetComponent<Rigidbody2D>();

        controls.Gamepad.Kick.performed += ctx => Kick();
        controls.Keyboard.Kick.performed += ctx => Kick();
        controls.Gamepad.Movement.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gamepad.Movement.canceled += ctx => move = Vector2.zero;
        controls.Keyboard.Movement.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Keyboard.Movement.canceled += ctx => move = Vector2.zero;
        controls.Keyboard.Jump.performed += ctx => Jump();
        controls.Gamepad.Jump.performed += ctx => Jump();
    }

    private void OnEnable()
    {
        controls.Gamepad.Enable();
        controls.Keyboard.Enable();
    }

    private void OnDisable()
    {
        controls.Gamepad.Disable();
        controls.Keyboard.Disable();
    }

    void Update()
    {
        ApplyGravity();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector2 m = new Vector2(move.x, move.y) * speed * Time.fixedDeltaTime;
        transform.Translate(m, Space.World);
    }

    void Kick()
    {
        Debug.Log("Kick");
    }

    void Jump()
    {
        Debug.Log("Jump");
        Vector2 jump = new Vector2(rb.linearVelocity.x, jumpForce);
        rb.AddForce(jump, ForceMode2D.Impulse);
    }

    void ApplyGravity()
    {
        if(rb.linearVelocity.y > 0)
        {
            rb.gravityScale = jumpGravity;
        }
        else if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = fallGravity;
        }
        else
        {
            rb.gravityScale = normalGravity;
        }
    }
}