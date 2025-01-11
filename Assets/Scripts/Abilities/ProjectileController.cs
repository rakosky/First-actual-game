using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    protected float speed;
    protected float maxRange;
    protected Vector2 direction;
    protected Vector2 startPosition;
    protected float rotationSpeed = 360f; // Degrees per second

    protected Rigidbody2D rb;
    

    public virtual void Init(float speed, float maxRange, Vector2 direction, Vector2 startPosition)
    {
        this.speed = speed;
        this.maxRange = maxRange;
        this.direction = direction;
        this.startPosition = startPosition;
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        Debug.Log(startPosition);

        rb.linearVelocity = new Vector2(this.direction.x * speed, 0);
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if (Vector2.Distance(startPosition, transform.position) >= maxRange)
        {
            Destroy(gameObject);
        }
    }
}
