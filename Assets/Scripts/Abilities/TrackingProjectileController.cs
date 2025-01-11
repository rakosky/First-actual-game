using UnityEngine;

public class TrackingProjectileController : ProjectileController
{
    private Transform target;

    public override void Init(float speed, float maxRange, Vector2 direction, Vector2 startPosition)
    {
        base.Init(speed, maxRange, direction, startPosition);

        target = FindClosestEnemy();
        if (target != null)
            Debug.Log("target locked");
    }

    protected override void Update()
    {
        base.Update();

        if (target == null)
            return;

        // Calculate the direction vector to the target
        Vector3 direction = (target.position - transform.position).normalized;

        // Calculate the movement step based on speed and time
        Vector3 movement = direction * speed * Time.deltaTime;

        // Move the projectile towards the target
        transform.position += movement;
    }

    private Transform FindClosestEnemy()
    {
        float detectionRange = 100f; // Range of detection
        float detectionAngle = 85f; // Cone angle in degrees
        LayerMask enemyLayer = LayerMask.GetMask("Enemy"); // Adjust to your enemy layer

        // Get all colliders in the detection range
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange, enemyLayer);

        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D hit in hits)
        {
            Vector2 directionToTarget = (hit.transform.position - transform.position).normalized;

            // Calculate the angle between the projectile's facing direction and the target
            float angleToTarget = Vector2.Angle(direction, directionToTarget);

            // Check if the target is within the cone angle
            if (angleToTarget <= detectionAngle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, hit.transform.position);
                if (distanceToTarget < closestDistance)
                {
                    closestDistance = distanceToTarget;
                    closestEnemy = hit.transform;
                }
            }
        }

        return closestEnemy;
    }
}
