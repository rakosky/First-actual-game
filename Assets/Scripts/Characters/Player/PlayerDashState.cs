using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerStateMachine stateMachine, Player player, string animationName) : base(stateMachine, player, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.characterDash.Dash();
        player.DisableInput();
    }

    public override void Exit()
    {
        base.Exit();

        player.EnableInput();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (!player.characterDash.IsDashing)
        {
            stateMachine.ChangeToState(player.idleState);
        }

    }
}
