using code.Helpers;
using code.StateMachines.CharacterStates.NPCStates;
using Godot;

public class Character : KinematicBody2D, IAttacker
{
    public const string ANIMATION_PLAYER_NAME = "AnimationPlayer";
    public const string AUDIO_STREAM_PLAYER_NAME = "AudioStreamPlayer2D";

    public const string PATH_TO_SOUNDS = "res://assets/audio/sounds/character/";

    public Vector2 currentMoveDirection;
    public Vector2 incomingAttackForce;

    // Node members
    public AnimationPlayer AnimationPlayer { get; protected set; }
    public AudioStreamPlayer2D AudioPlayer { get; protected set; }
    public Hitbox Hitbox { get { return this.GetNode<Hitbox>("Hitbox"); } }
    // Custom components
    [Export]
    public CharacterStats BaseStats { get; protected set; }
    [Export]
    public CharacterStats CurrentStats { get; protected set; }
    public PositionalSoundPlayer SoundPlayer;

    // Custom properties
    [Export]
    public bool Invulnerable { get; set; }

    protected BaseFSMState currentState;
    protected Attack currentAttack;

    public override void _Ready()
    {
        this.InitNodes();
        this.InitDefaultProperties();
        this.InitState();
    }

    /// <summary>
    ///     Inits node members according to expected default names.
    /// </summary>
    protected virtual void InitNodes()
    {
        this.AnimationPlayer = this.GetNode<AnimationPlayer>(ANIMATION_PLAYER_NAME);
        this.AudioPlayer = this.GetNode<AudioStreamPlayer2D>(AUDIO_STREAM_PLAYER_NAME);

        SoundPlayer = new PositionalSoundPlayer(AudioPlayer, PATH_TO_SOUNDS);
    }
    protected virtual void InitDefaultProperties()
    {
        // For this game default mp and defense to 0 - attack will always be determined by stength
        this.BaseStats = new CharacterStats(3, 3, 1, 0, 1.5f);
        this.CurrentStats = this.BaseStats;

        this.currentAttack = new Attack(this, CurrentStats.Strength, 50);
    }
    protected virtual void InitState()
    {
        this.currentState = new Idle(this);
    }

    public void SetCharacterOrientation(Vector2 moveDirection)
    {
        if (!moveDirection.Equals(Vector2.Zero))
        {
            this.Rotation = moveDirection.Angle();
        }
    }

    public virtual void Die()
    {
        //EmitSignal(nameof(EnemyDied), this);
        this.QueueFree();
    }

    public int GetPointValue()
    {
        return (int)(BaseStats.MaxHp + BaseStats.MaxMp + BaseStats.Strength + BaseStats.Defense + BaseStats.Speed);
    }

    public Attack Attack => currentAttack;
    /// <summary>
    /// The node adminstering the attack (just the parent object like the bullet or the parent of melee attack)
    /// </summary>
    public Node2D Attacker => this;
}
