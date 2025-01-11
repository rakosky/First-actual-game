using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float detectionRange = 5f;
    public float attackRange = 1.5f;

    protected EnemyMovement movement;
    protected TargetDetection targetDetection;
    protected Animator anim;
    //private EnemyAttack attack;

    protected virtual void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        targetDetection = GetComponent<TargetDetection>();
        anim = GetComponentInChildren<Animator>();
        //  attack = GetComponent<EnemyAttack>();
    }

    protected virtual void Update()
    {

    }

    public void DisableEnemy()
    {
        movement.Stop();
        //attack.Disable();
    }
}
