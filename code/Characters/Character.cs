using code.StateMachines.CharacterStates.NPCStates;
using Godot;

public class Character : KinematicBody2D, IHurtable, IAttacker
{
    const string ANIMATION_PLAYER_NAME = "AnimationPlayer";

    public Vector2 currentMoveDirection;

    // Node members
    public AnimationPlayer AnimationPlayer { get; protected set; }
    // Custom components
    [Export]
    public CharacterStats BaseStats { get; protected set; }
    [Export]
    public CharacterStats CurrentStats { get; protected set; }

    // Custom properties
    [Export]
    public bool Invulnerable { get; set; }

    protected BaseFSMState currentState;
    protected Attack currentAttack;

    public override void _Ready()
    {
        this.InitNodes();

        this.BaseStats = new CharacterStats(3, 0, 1, 0, 5);
        this.CurrentStats = this.BaseStats;

        this.currentState = new Idle(this);
        this.currentAttack = new Attack(1, 0);
    }

    /// <summary>
    ///     Inits node members according to expected default names.
    /// </summary>
    protected void InitNodes()
    {
        this.AnimationPlayer = this.GetNode<AnimationPlayer>(ANIMATION_PLAYER_NAME);
    }

    public void SetCharacterOrientation(Vector2 moveDirection)
    {
        if (!moveDirection.Equals(Vector2.Zero))
        {
            this.Rotation = moveDirection.Angle();
        }
    }

    public void ApplyIncomingAttack(Node2D attacker, Attack attack)
    {
        if (currentState is IHurtable hurtable) hurtable.ApplyIncomingAttack(attacker, attack);
    }

    public void TakeDamage(int hpAmount)
    {
        if (currentState is IHurtable hurtable) hurtable.TakeDamage(hpAmount);
    }

    public Attack Attack => currentAttack;
    public Node2D Attacker => this;
}
