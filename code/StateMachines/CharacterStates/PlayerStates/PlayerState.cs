using Godot;

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
    /// <param name="delta">Time since previous game loop in seconds.</param>
    /// <returns>State after input is processed.</returns>
    public abstract BaseFSMState HandleInput(float delta);
    /// <summary>
    ///     Processes player input and updates the players state.
    /// </summary>
    /// <param name="delta">Time since previous game loop in seconds.</param>
    /// <returns>State after the update is performed.</returns>
    public BaseFSMState HandleInputAndUpdate(float delta) 
    {
        return this.HandleInput(delta).Update(delta);
    }

    protected override void ExitState()
    {
        player.AnimationPlayer.Play("RESET");
    }

    protected float NormalizeRotationDegrees(float degrees)
    {
        if (degrees < 0)
        {
            return 360-Mathf.Abs(degrees);
        }
        else
        {
            return degrees;
        }
    }
}
