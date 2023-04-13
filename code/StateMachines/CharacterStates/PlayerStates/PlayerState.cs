public abstract class PlayerState : BaseFSMState
{
    protected Player player;

    public PlayerState(Player player)
    {
        this.player = player;
    }

    /// <summary>
    ///     Processes player input and updates current state.
    /// </summary>
    /// <returns>State after input is processed.</returns>
    public abstract BaseFSMState HandleInput();
    /// <summary>
    ///     Processes player input and updates the players state.
    /// </summary>
    /// <param name="delta">Time since previous game loop in seconds.</param>
    /// <returns>State after the update is performed.</returns>
    public BaseFSMState HandleInputAndUpdate(float delta) 
    {
        return this.HandleInput().Update(delta);
    }
}
