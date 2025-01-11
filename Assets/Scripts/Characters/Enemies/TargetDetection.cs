using UnityEngine;

public class TargetDetection : MonoBehaviour
{
    public Transform target;
    public float targetDetectionRange = 15f;
    public float targetAttackRange = 4f;

    private void Awake()
    {
    }

    public bool TargetDetected()
    {
        return Vector2.Distance(transform.position, target.position) <= targetDetectionRange;

    }
    public bool TargetInAttackRange()
    {
        return Vector2.Distance(transform.position, target.position) <= targetAttackRange;
    }



}
