using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(PlayerStateMachine stateMachine, Player player, string animationName) : base(stateMachine, player, animationName)
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

        if (player.inputs.Jumped)
        {
            stateMachine.ChangeToState(player.jumpState);
            return;
        }

        var movement = player.inputs.Movement;
        if (movement == Vector2.zero)
        {
            stateMachine.ChangeToState(player.idleState);
            return;
        }
    }
}
