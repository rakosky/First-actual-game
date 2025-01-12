using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    ProjectileAbility ability;
    GameObject user;
    protected float speed;
    protected float maxRange;
    protected Vector2 direction;
    protected Vector2 startPosition;
    protected float rotationSpeed = 360f;

    protected Rigidbody2D rb;
    

    public virtual void Init(ProjectileAbility ability, GameObject user, Vector2 direction, Vector2 startPosition)
    {
        this.ability = ability;
        this.user = user;
        this.speed = ability.projectileSpeed;
        this.maxRange = ability.projectileRange;
        this.direction = direction;
        this.startPosition = startPosition;
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        rb.linearVelocity = new Vector2(this.direction.x * speed, 0);
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if (Vector2.Distance(startPosition, transform.position) >= maxRange)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() is Enemy enemy)
        {
            user.GetComponent<CharacterStats>()?.DealDamage(enemy.stats, DamageType.Physical, ability.damagePerHit);
            Destroy(gameObject);
        }
    }
}
