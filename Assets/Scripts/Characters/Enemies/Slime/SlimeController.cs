using Assets.Scripts.Stats;
using System.Collections;
using System.Threading;
using UnityEngine;

public class SlimeController : EnemyController
{
    public enum State { Idle, Roaming, Persuing, Attacking, Hit, Dying }
    private State currentState = State.Idle;

    private float hitTimer = 0;


    protected override void Awake()
    {
        base.Awake();
        animationEvents = GetComponentInChildren<EnemyAnimationEvents>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        animationEvents.OnAttack1Hit += OnAttack1Hit;
        animationEvents.OnAttack1Finished += OnAttack1Finished;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
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
                anim.SetBool("hit", false);

                if (targetDetection.TargetDetected())
                    currentState = State.Persuing;
                else if (!movement.IsRomeOnCooldown)
                    currentState = State.Roaming; 

                break;

            case State.Roaming:
                anim.SetBool("move", true);
                anim.SetBool("idle", false);
                anim.SetBool("hit", false);

                movement.MoveToRoamPosition();
                if (targetDetection.TargetDetected())
                    currentState = State.Persuing;
                else if (movement.ReachedRoamPosition)
                    currentState = State.Idle;

                break;

            case State.Persuing:
                anim.SetBool("idle", false);
                anim.SetBool("move", true);
                anim.SetBool("hit", false);

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
                anim.SetBool("hit", false);

                break;

            case State.Hit:
                movement.Stop();
                anim.SetBool("idle", false);
                anim.SetBool("move", false);
                anim.SetBool("attack1", false);
                anim.SetBool("hit", true);
                if ((hitTimer -= Time.deltaTime) <= 0)
                    currentState = State.Idle;
                
                break;

            case State.Dying:
                anim.SetBool("idle", false);
                anim.SetBool("move", false);
                anim.SetBool("attack1", false);
                anim.SetBool("hit", false);
                anim.SetBool("die", true);

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

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        currentState = State.Hit;
        hitTimer = .5f;
    }
}