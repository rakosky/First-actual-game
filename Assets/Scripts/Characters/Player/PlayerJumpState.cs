using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerStateMachine stateMachine, Player player, string animationName) : base(stateMachine, player, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.characterMovement.Jump();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        var movement = player.inputs.Movement;
        player.characterMovement.Move(movement);
    }

    public override void Update()
    {
        base.Update();

        if (player.rb.linearVelocityY < 0)
        {
            stateMachine.ChangeToState(player.fallState);
        }

    }
}
