using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected string animationName;
    protected float stateTimer;

    protected PlayerState(PlayerStateMachine stateMachine, Player player, string animationName)
    {
        this.stateMachine = stateMachine;
        this.player = player;
        this.animationName = animationName;
    }

    public virtual void Enter()
    {
        Debug.Log($"Entering {GetType()} state.");
    }

    public virtual void Exit()
    {
        Debug.Log($"Exiting {GetType()} state.");
    }

    public virtual void Update()
    {
        if (player.inputs.Dashed)
        {
            stateMachine.ChangeToState(player.dashState);
            return;
        }
    }

    public virtual void FixedUpdate()
    {
    }

}
