using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputController : InputController
{
    private PlayerInput input;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction dashAction;

    public new PlayerInputs Inputs = new PlayerInputs();

    protected override void Awake()
    {
        base.Awake();
        
        input = GetComponent<PlayerInput>();

        moveAction = input.actions["Move"];
        jumpAction = input.actions["Jump"];
        dashAction = input.actions["Dash"];
    }

    protected override void Update()
    {
        base.Update();

        if (IsDisabled)
        {
            Inputs = new();
            return;
        }

        Inputs.UpdateInputs(
            movement: moveAction.ReadValue<Vector2>(),
            jumped: jumpAction.triggered,
            dashed: dashAction.triggered,
            paused: false);
    }

}

public class PlayerInputs : CharacterInputs
{
    // additional player inputs
    public bool Paused { get; private set; }
    public void UpdateInputs(Vector2 movement, bool jumped, bool dashed, bool paused)
    {
        base.UpdateInputs(movement, jumped, dashed);
        Paused = paused;
    }
}