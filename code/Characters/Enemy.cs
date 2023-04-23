using code.StateMachines.CharacterStates.EnemyStates;
using Godot;

public class Enemy : Character, IHurtable
{
    [Signal]
    public delegate void EnemyDied(Enemy enemy);

    public NavigationAgent2D NavigationAgent { get { return this.GetNode<NavigationAgent2D>("NavigationAgent2D"); } }
    public Character currentTarget;

    protected override void InitState()
    {
        this.currentState = new Idle(this);
    }

    public override void _PhysicsProcess(float delta)
    {
        currentState = (EnemyState)currentState.Update(delta);
    }

    public void ApplyIncomingAttack(Node2D attacker, Attack attack)
    {
        if (!Invulnerable)
        {
            currentState = currentState.SwitchState(new TakingDamage(this));
            int damage = attack.Damage - this.CurrentStats.Defense;
            this.TakeDamage(damage);
            if (attack.PushForce > 0)
            {
                Vector2 hitDirection = (this.GlobalPosition - attacker.GlobalPosition).Normalized();
                this.incomingAttackForce = hitDirection * attack.PushForce;
            }
        }
        currentTarget = attack.Owner;
    }
    public void TakeDamage(int hpAmount)
    {
        if (this.CurrentStats.ApplyDamage(hpAmount))
        {
            // When hp is depleted
            currentState = currentState.SwitchState(new Dying(this));
        }
    }

    public void Die()
    {
        EmitSignal(nameof(EnemyDied), this);
        this.QueueFree();
    }
}
