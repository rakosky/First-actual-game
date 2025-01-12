using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tracking Projectile", menuName = "Abilities/Ranged/Tracking")]
public class TrackingProjectileAbility : ProjectileAbility
{
    protected override void Activate(GameObject user)
    {
        base.Activate(user);

        user.GetComponent<MonoBehaviour>().StartCoroutine(FireProjectiles(user));
    }

    private IEnumerator FireProjectiles(GameObject user)
    {
        for (int i = 0; i < hitCount; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, usedPosition, Quaternion.identity);
            var controller = projectile.AddComponent<TrackingProjectileController>();
            controller.Init(this, user, usedDirection, usedPosition);
            yield return new WaitForSeconds(projectileFireDelay);
        }
    }
}
