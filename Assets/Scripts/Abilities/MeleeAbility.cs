using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Ability", menuName = "Abilities/Melee")]
public class MeleeAbility : Ability
{
    public float damage;
    public float range;

    protected override void Activate(GameObject user)
    {
        // Implement melee attack logic
        Debug.Log($"{abilityName} activated! Dealing {damage} damage.");

        // You can add logic here to detect nearby enemies and deal damage
    }
}
