using UnityEngine;

public abstract class InputController : MonoBehaviour
{
    public bool IsDisabled { get; set; }
    public CharacterInputs Inputs { get; private set; } = new CharacterInputs();

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {

    }
}

public class CharacterInputs
{
    public Vector2 Movement { get; private set; }
    public bool Jumped { get; private set; }
    public bool Dashed { get; private set; }

    public virtual void UpdateInputs(Vector2 movement, bool jumped, bool dashed)
    {
        Movement = movement;
        Jumped = jumped;
        Dashed = dashed;
    }

    public static CharacterInputs Empty => new();

}