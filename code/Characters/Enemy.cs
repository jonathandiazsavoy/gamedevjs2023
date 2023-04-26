using code.StateMachines.CharacterStates.EnemyStates;
using Godot;

public class Enemy : Character, IHurtable
{
    [Export]
    public int MaxHp=3;
    [Export]
    public int Strength=1;
    [Export]
    public float Speed=1.3f;

    [Export]
    public float WaitAfterBumpingPlayer = 1f;

    [Signal]
    public delegate void EnemyDied(Enemy enemy);

    public NavigationAgent2D NavigationAgent { get { return this.GetNode<NavigationAgent2D>("NavigationAgent2D"); } }
    public Character currentTarget;
    public bool Alerted;

    protected override void InitState()
    {
        this.currentState = new Sleeping(this);
        this.currentState = this.currentState.SwitchState(new Sleeping(this));
        Alerted = false;
    }
    protected override void InitDefaultProperties()
    {
        // For this game default mp and defense to 0 - attack will always be determined by stength
        this.BaseStats = new CharacterStats(this.MaxHp, 0, this.Strength, 0 , this.Speed);
        this.CurrentStats = this.BaseStats;

        this.currentAttack = new Attack(this, CurrentStats.Strength, 50);
    }

    public override void _PhysicsProcess(float delta)
    {
        currentState = (EnemyState)currentState.Update(delta);
    }

    public virtual void ApplyIncomingAttack(Node2D attacker, Attack attack)
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
    public virtual void TakeDamage(int hpAmount)
    {
        if (this.CurrentStats.ApplyDamage(hpAmount))
        {
            // When hp is depleted
            currentState = currentState.SwitchState(new Dying(this));
        }
    }

    public override void Die()
    {
        EmitSignal(nameof(EnemyDied), this);
        this.QueueFree();
    }

    // **************************************************
    // Signal listeners
    // **************************************************
    public void OnPlayerBumped(Area2D area2d)
    {
        if (!(area2d is Hurtbox)) return; // Ignore anything not hurtbox 
        if (area2d.GetParent() is Player)
        {
            this.currentState = currentState.SwitchState(new BumpedTarget(this));
        }
    }
}
