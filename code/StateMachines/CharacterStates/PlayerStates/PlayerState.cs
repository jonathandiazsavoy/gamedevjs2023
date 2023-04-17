using code.StateMachines.CharacterStates.PlayerStates;
using Godot;

public abstract class PlayerState : BaseFSMState, IHurtable
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

    public void ApplyIncomingAttack(Node2D attacker, Attack attack)
    {
        this.SwitchState(new TakingDamage(player));
        int damage = attack.Damage - player.CurrentStats.Defense;
        player.TakeDamage(damage);
        if (attack.PushForce > 0)
        {
            Vector2 hitDirection = (player.GlobalPosition - attacker.GlobalPosition).Normalized();
            player.MoveAndSlide(hitDirection * attack.PushForce);
        }
    }

    public void TakeDamage(int hpAmount)
    {
        if (player.CurrentStats.ApplyDamage(hpAmount))
        {
            // When hp is depleted
            this.SwitchState(new Dying(player));
        }
    }
}
