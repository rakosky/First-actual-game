using UnityEngine;

public class TrackingProjectileController : ProjectileController
{
    private Character target;

    public override void Init(ProjectileAbility ability, GameObject user, Vector2 direction, Vector2 startPosition)
    {
        base.Init(ability, user, direction, startPosition);

        target = FindClosestTarget();
        if(target != null) 
            target.OnDie += ClearTarget;
    }

    protected override void Update()
    {
        base.Update();

        if (target == null)
            return;

        Vector3 direction = (target.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private Character FindClosestTarget()
    {
        float detectionRange = maxRange; // Range of detection
        float detectionAngle = 85f; // Cone angle in degrees
        LayerMask enemyLayer = LayerMask.GetMask("Enemy");

        // Get all colliders in the detection range
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange, enemyLayer);

        Character closestTarget = null;
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
                    closestTarget = hit.GetComponent<Character>();
                }
            }
        }

        return closestTarget;
    }

    private void ClearTarget()
    {
        Debug.Log("clearin targ");
        if (target != null)
            target = null;
    }
}
