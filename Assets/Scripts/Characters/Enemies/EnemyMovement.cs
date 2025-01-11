using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float roamSpeed = 2f;
    public float chaseSpeed = 3.5f;
    public float minRoamDistance = 8f;
    public float maxRoamDistance = 16f;
    public float idleAfterRomeDuration = 2f;

    [HideInInspector] public bool IsRomeOnCooldown;
    public Vector2 roamPosition;
    public bool ReachedRoamPosition => Vector2.Distance(transform.position, roamPosition) < 0.1f;

    private Enemy enemy;

    public bool isMovementEnabled = true;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        SetNewRoamPosition();
    }

    public void MoveToRoamPosition()
    {
        if (!isMovementEnabled || IsRomeOnCooldown)
            return;

        if (ReachedRoamPosition)
            StartCoroutine(WaitBeforeNewRoam());
        else
            MoveToTarget(roamPosition, roamSpeed);
    }

    private void SetNewRoamPosition()
    {
        if (!isMovementEnabled) return;

        var randomDistanceX = Random.Range(minRoamDistance, maxRoamDistance);
        randomDistanceX *= (Random.Range(0f, 1f) > 0.5f)
            ? 1
            : -1;

        roamPosition = new Vector2(
            transform.position.x + randomDistanceX,
            transform.position.y);
    }

    public void Patroll()
    {
        if (!isMovementEnabled) return;
    }

    public void Jump()
    {
        if (!isMovementEnabled) return;
    }

    public void MoveToTarget(Vector2 target, float speed)
    {
        if (!isMovementEnabled) return;

        Vector2 direction = (target - (Vector2)transform.position).normalized;
        enemy.SetVelocity(direction * speed);
    }

    public void Stop()
    {
        if (!isMovementEnabled) return;

        enemy.SetVelocity(Vector2.zero);
    }

    private IEnumerator WaitBeforeNewRoam()
    {
        if (!isMovementEnabled) yield break;

        IsRomeOnCooldown = true;
        yield return new WaitForSeconds(idleAfterRomeDuration);
        IsRomeOnCooldown = false;
        SetNewRoamPosition();
    }
}
