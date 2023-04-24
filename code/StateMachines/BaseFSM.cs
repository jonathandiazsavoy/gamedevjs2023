using Godot;

public abstract class BaseFSMState
{
    /// <summary>
    ///     Update the current state.
    /// </summary>
    /// <param name="delta">Time since previous game loop in seconds.</param>
    /// <returns>State after the update is performed.</returns>
    public abstract BaseFSMState Update(float delta);
    protected abstract void EnterState();
    protected virtual void ExitState() { return; }
    /// <summary>
    ///     Handle state switch and return the new state.
    /// </summary>
    /// <param name="newState">State to switch to.</param>
    /// <returns>Newly switched state.</returns>
    public BaseFSMState SwitchState(BaseFSMState newState)
    {
        //TODO debug
        //GD.Print("switch state from: " + this+" to: " + newState);
        this.ExitState();
        newState.EnterState();
        return newState;
    }
}