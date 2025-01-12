using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float detectionRange = 5f;
    public float attackRange = 1.5f;

    protected EnemyMovement movement;
    protected TargetDetection targetDetection;
    protected Animator anim;
    protected Enemy enemy;
    protected EnemyAnimationEvents animationEvents;


    protected virtual void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        targetDetection = GetComponent<TargetDetection>();
        anim = GetComponentInChildren<Animator>();
        enemy = GetComponent<Enemy>();
    }
    protected virtual void OnEnable()
    {
        enemy.OnTakeDamage += TakeDamage;
        enemy.OnDie += Die;
        animationEvents.OnDieFinished += DieFinished;
    }

    protected virtual void OnDisable()
    {
        enemy.OnTakeDamage -= TakeDamage;
        enemy.OnDie -= Die;
        animationEvents.OnDieFinished += DieFinished;
    }

    protected virtual void Update()
    {

    }

    public void DisableEnemy()
    {
        movement.Stop();
        enemy.GetComponent<Collider2D>().enabled = false;
        enemy.rb.bodyType = RigidbodyType2D.Static;
    }

    public virtual void Die()
    {
        anim.SetBool("die", true);
        DisableEnemy();
    }

    private void DieFinished()
    {
        Destroy(gameObject);
    }

    public virtual void TakeDamage(int damage)
    {

    }
}
