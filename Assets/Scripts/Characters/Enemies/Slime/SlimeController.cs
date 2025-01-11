using Assets.Scripts.Stats;
using UnityEngine;

public class SlimeController : EnemyController
{
    public enum State { Idle, Roaming, Persuing, Attacking }
    private State currentState = State.Idle;

    private Enemy enemy;
    private EnemyAnimationEvents animationEvents;


    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponent<Enemy>();
        animationEvents = GetComponentInChildren<EnemyAnimationEvents>();
    }

    private void OnEnable()
    {
        animationEvents.OnAttack1Hit += OnAttack1Hit;
        animationEvents.OnAttack1Finished += OnAttack1Finished;
    }

    private void OnDisable()
    {
        animationEvents.OnAttack1Hit -= OnAttack1Hit;
        animationEvents.OnAttack1Finished -= OnAttack1Finished;
    }

    protected override void Update()
    {
        base.Update();

        switch (currentState)
        {
            case State.Idle:
                anim.SetBool("move", false);
                anim.SetBool("idle", true);
                if (targetDetection.TargetDetected())
                    currentState = State.Persuing;
                else if (!movement.IsRomeOnCooldown)
                    currentState = State.Roaming; 

                break;

            case State.Roaming:
                anim.SetBool("move", true);
                anim.SetBool("idle", false);
                movement.MoveToRoamPosition();
                if (targetDetection.TargetDetected())
                    currentState = State.Persuing;
                else if (movement.ReachedRoamPosition)
                    currentState = State.Idle;

                break;

            case State.Persuing:
                anim.SetBool("idle", false);
                anim.SetBool("move", true);
                //impl max chase duration

                if (targetDetection.TargetInAttackRange())
                    currentState = State.Attacking;
                else if (!targetDetection.TargetDetected())
                    currentState = State.Roaming;
                else
                    movement.MoveToTarget(targetDetection.target.transform.position, movement.chaseSpeed);

                break;

            case State.Attacking:
                movement.Stop();
                anim.SetBool("idle", false);
                anim.SetBool("move", false);
                anim.SetBool("attack1", true);
                break;
        }
    }

    private void OnAttack1Hit()
    {
        Debug.Log("Hit!");
        enemy.stats.DealDamage(PlayerManager.instance.Player.stats, DamageType.Physical);
        
    }
    private void OnAttack1Finished()
    {
        anim.SetBool("attack1", false);
        currentState = State.Persuing;
    }
}