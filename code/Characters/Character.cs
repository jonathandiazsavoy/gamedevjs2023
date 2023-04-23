using code.Helpers;
using code.StateMachines.CharacterStates.NPCStates;
using Godot;

public class Character : KinematicBody2D, IAttacker
{
    const string ANIMATION_PLAYER_NAME = "AnimationPlayer";
    const string AUDIO_STREAM_PLAYER_NAME = "AudioStreamPlayer2D";

    public const string PATH_TO_SOUNDS = "res://assets/audio/sounds/character/";

    public Vector2 currentMoveDirection;
    public Vector2 incomingAttackForce;

    // Node members
    public AnimationPlayer AnimationPlayer { get; protected set; }
    public AudioStreamPlayer2D AudioPlayer { get; protected set; }
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

        this.BaseStats = new CharacterStats(3, 0, 1, 0, 5);
        this.CurrentStats = this.BaseStats;

        this.currentState = new Idle(this);
        this.currentAttack = new Attack(1, 0);
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

    public void SetCharacterOrientation(Vector2 moveDirection)
    {
        if (!moveDirection.Equals(Vector2.Zero))
        {
            this.Rotation = moveDirection.Angle();
        }
    }

    public int GetPointValue()
    {
        return (int)(BaseStats.MaxHp + BaseStats.MaxMp + BaseStats.Strength + BaseStats.Defense + BaseStats.Speed);
    }

    public Attack Attack => currentAttack;
    public Node2D Attacker => this;
}
