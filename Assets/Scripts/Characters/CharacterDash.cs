using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]

public class CharacterDash : MonoBehaviour
{
    [SerializeField] private float dashSpeedFactor = 4f;
    [SerializeField] private float dashDuration = .5f;
    [SerializeField] private float dashCooldown = 1f;
    private float dashCooldownTimer;
    private float dashTimer = -1f;
    private Character character;

    public bool IsDashing;
    public float initialGravityScale;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Update()
    {
        dashCooldownTimer -= Time.deltaTime;

        if (dashTimer > 0)
        {
            IsDashing = true;
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0)
                DashEnd();
        }
    }

    private void FixedUpdate()
    {
        if (!IsDashing)
            return;

        character.SetVelocity(new Vector2(
            character.characterMovement.moveSpeed * character.facingDirection * dashSpeedFactor,
            0));
    }

    public void Dash()
    {
        if (dashCooldownTimer > 0)
            return;

        dashCooldownTimer = dashCooldown;
        dashTimer = dashDuration;
        initialGravityScale = character.rb.gravityScale;
        character.rb.gravityScale = 0f;
        character.isInputBlocked = true;
    }

    private void DashEnd()
    {
        character.rb.gravityScale = initialGravityScale;
        character.SetVelocity(Vector2.zero);
        IsDashing = false;
        character.isInputBlocked = false;
    }
}
