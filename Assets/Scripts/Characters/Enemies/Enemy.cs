using UnityEngine;

public class Enemy : Character
{
    private EnemyController controller;

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<EnemyController>();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("ow!");

        //health.ReduceHealth(damage);

        //if (health.IsDead)
        //{
        //    Die();
        //}
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        controller.DisableEnemy();
        Destroy(gameObject, 2f); // Delay destruction for death animation/effects
    }
}
