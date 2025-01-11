using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 20f;

    [SerializeField] private float airDragFactor = .3f;

    private Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    public void Move(Vector2 movement)
    {
        if (character.isGrounded)
        {
            character.SetVelocityX(movement.x * moveSpeed);
        }
        else
        {
            var newVelocityX = Mathf.Lerp(character.rb.linearVelocityX, movement.x * moveSpeed, airDragFactor);

            character.SetVelocityX(newVelocityX);
        }

    }
    
    public void Jump()
    {
        character.SetVelocityY(jumpForce);
    }
}
