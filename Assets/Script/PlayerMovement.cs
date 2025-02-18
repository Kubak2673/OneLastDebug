using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Ustawienia ruchu")]
    [SerializeField] private float walkSpeed = 5.5f;
    [SerializeField] private float jumpHeight = 9.5f;
    [SerializeField] private float deceleration = 10f;

    [SerializeField] private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private float horizontal;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (boxCollider == null)
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }
    }


    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        Walk();
        HandleJump();
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (boxCollider != null && !boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
                return;
            Jump();
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
    }

    void Walk()
    {
        if (Mathf.Abs(horizontal) < 0.01f)
        {
            rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocity.x, 0, deceleration * Time.deltaTime), rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(horizontal * walkSpeed, rb.linearVelocity.y);
        }
    }

}
