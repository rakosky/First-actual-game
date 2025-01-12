using UnityEngine;

public class Enemy : Character
{
    private EnemyController controller;

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<EnemyController>();
    }
}
