using System;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    public event Action OnAttack1Hit;
    public event Action OnAttack1Finished;
    public event Action OnDieFinished;

    private void OnAttack1Trigger()
    {
        OnAttack1Hit?.Invoke();
    }

    private void OnAttack1FinishedTrigger()
    {
        OnAttack1Finished?.Invoke();
    }

    private void OnDieFinish()
    {
        OnDieFinished?.Invoke();
    }

}

