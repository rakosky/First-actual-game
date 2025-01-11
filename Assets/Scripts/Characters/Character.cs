using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public CharacterMovement characterMovement;
    [HideInInspector] public CharacterDash characterDash;
    [HideInInspector] public CharacterStats stats;

    [SerializeField] private float groundCheckDistance = .5f;
    private LayerMask whatIsGround;
    public bool isGrounded;

    public bool isInputBlocked;
    public int facingDirection = 1;
    public event Action OnFlipped;



    protected virtual void Update()
    {
        isGrounded = CheckIfGrounded();
    }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        characterMovement = GetComponent<CharacterMovement>();
        characterDash = GetComponent<CharacterDash>();
        stats = GetComponent<CharacterStats>();

        whatIsGround = LayerMask.GetMask("Ground");
    }

    protected virtual void Start()
    {
    }

    public virtual void SetVelocity(Vector2 velocity, bool flip = true)
    {
        rb.linearVelocity = velocity;

        if (flip)
            FlipController();
    }

    public void SetVelocityX(float xVelocity) => SetVelocity(new Vector2(xVelocity, rb.linearVelocityY));
    public void SetVelocityY(float yVelocity) => SetVelocity(new Vector2(rb.linearVelocityX, yVelocity));

    private bool CheckIfGrounded()
    {
        // Cast a ray downward to check if the player is on the ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);

        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, hit.collider ? Color.green : Color.red);

        return hit.collider != null;
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0, 180, 0);

        // UI updates and whatever else
        OnFlipped?.Invoke();
    }

    protected virtual void FlipController()
    {
        if ((rb.linearVelocityX > 0 && facingDirection == -1)
          || (rb.linearVelocityX < 0 && facingDirection == 1))
        {
            Flip();
        }
    }

    public virtual void Die()
    {
        Debug.Log(GetType() + " died");
    }

}
