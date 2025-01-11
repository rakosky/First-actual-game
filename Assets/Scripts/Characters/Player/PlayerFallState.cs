using UnityEngine;

public class PlayerFallState : PlayerState
{
    public PlayerFallState(PlayerStateMachine stateMachine, Player player, string animationName) : base(stateMachine, player, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
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

        if (player.isGrounded)
        {
            stateMachine.ChangeToState(player.idleState);
        }

    }
}
