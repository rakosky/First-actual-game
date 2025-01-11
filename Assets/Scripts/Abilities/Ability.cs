using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public float cooldown;
    public Sprite icon;

    protected Vector2 usedPosition;
    protected Vector2 usedDirection;
    protected float lastUsedTime;

    private void OnEnable()
    {
        lastUsedTime = -Mathf.Infinity;
    }

    public bool CanUse()
    {
        return Time.time >= lastUsedTime + cooldown;
    }

    public void Use(GameObject user)
    {
        if (CanUse())
        {
            usedPosition = user.transform.position;
            usedDirection = user.transform.right;
            lastUsedTime = Time.time;
            Activate(user);
        }
        else
        {
            Debug.Log($"{abilityName} is on cooldown!");
        }
    }

    protected abstract void Activate(GameObject user);
}
