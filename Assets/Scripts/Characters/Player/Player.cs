using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    #region components
    [HideInInspector] public PlayerInputController input;
    public PlayerInputs inputs => input.Inputs;
    private PlayerStateMachine stateMachine;
    #endregion

    #region states
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerFallState fallState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    #endregion


    protected override void Awake()
    {
        base.Awake();
        input = GetComponent<PlayerInputController>();
        stateMachine = GetComponent<PlayerStateMachine>();



        idleState = new PlayerIdleState(stateMachine, this, "");
        moveState = new PlayerMoveState(stateMachine, this, "");
        jumpState = new PlayerJumpState(stateMachine, this, "");
        fallState = new PlayerFallState(stateMachine, this, "");
        dashState = new PlayerDashState(stateMachine, this, "");
    }


    protected override void Start()
    {
        base.Start();

        stateMachine.InitializeState(idleState);
    }

    protected override void Update()
    {
        base.Update();
        FlipController();
    }

    public override void SetVelocity(Vector2 velocity, bool flip = true)
    {
        base.SetVelocity(
            velocity,
            flip: false);
    }

    protected override void FlipController()
    {
        if (inputs.Movement.x == 0)
            return;

        if (facingDirection != inputs.Movement.x)
            Flip();
    }

    public void DisableInput() => input.IsDisabled = true;
    public void EnableInput() => input.IsDisabled = false;
}
