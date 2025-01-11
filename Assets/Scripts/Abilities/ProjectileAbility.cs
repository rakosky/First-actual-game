using UnityEngine;
using System.Collections;

public abstract class ProjectileAbility : Ability
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float projectileRange;
    public int projectileCount; // Number of projectiles to fire
    public float projectileFireDelay; // Delay between projectiles in seconds
    public float projectileGravity;

    protected override void Activate(GameObject user)
    {
        Debug.Log($"{abilityName} activated!");
        Debug.Log($"used direction {usedDirection}");
        Debug.Log($"used position {usedPosition}");
    }


}
