using code.StateMachines.CharacterStates.EnemyStates.FlyingEnemyStates;
using Godot;

public class FlyingEnemy : Enemy
{
    protected override void InitState()
    {
        this.currentState = new Sleeping(this);
        this.currentState = this.currentState.SwitchState(new Sleeping(this));
        Alerted = false;
    }

    public override void _PhysicsProcess(float delta)
    {
        currentState = (FlyingEnemyState)currentState.Update(delta);
    }

    public override void ApplyIncomingAttack(Node2D attacker, Attack attack)
    {
        if (!Invulnerable && !(attack.Owner is Enemy))
        {
            currentState = currentState.SwitchState(new TakingDamage(this));
            int damage = attack.Damage - this.CurrentStats.Defense;
            this.TakeDamage(damage);
            if (attack.PushForce > 0)
            {
                Vector2 hitDirection = (this.GlobalPosition - attacker.GlobalPosition).Normalized();
                this.incomingAttackForce = hitDirection * attack.PushForce;
            }
            currentTarget = attack.Owner;
        }
    }
    public override void TakeDamage(int hpAmount)
    {
        if (this.CurrentStats.ApplyDamage(hpAmount))
        {
            // When hp is depleted
            currentState = currentState.SwitchState(new Dying(this));
        }
    }

    // **************************************************
    // Signal listeners
    // **************************************************
    public new void OnPlayerBumped(Area2D area2d)
    {
        if (!(area2d is Hurtbox)) return; // Ignore anything not hurtbox 
        if (area2d.GetParent() is Player)
        {
            this.currentState = currentState.SwitchState(new BumpedTarget(this));
        }
    }
}
