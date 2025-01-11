using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerState current;

    public void InitializeState(PlayerState state)
    {
        current = state;
    }
    public void ChangeToState(PlayerState state)
    {
        current.Exit();

        current = state;

        current.Enter();
    }

    protected virtual void Update()
    {
        current.Update();
    }

    protected virtual void FixedUpdate()
    {
        current.FixedUpdate();
    }
}
