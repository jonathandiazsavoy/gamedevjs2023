using code.Helpers;
using code.Items;
using code.Items.TimeItems;
using code.StateMachines.CharacterStates.PlayerStates;
using Godot;

public class Player : Character, IHurtable, IObtainer
{
    [Signal]
    public delegate void PlayerDied(Player player);

    private GameManager gameManager;

    protected override void InitNodes()
    {
        this.AnimationPlayer = this.GetNode<AnimationPlayer>(Character.ANIMATION_PLAYER_NAME);
        this.AudioPlayer = this.GetNode<AudioStreamPlayer2D>(Character.AUDIO_STREAM_PLAYER_NAME);

        SoundPlayer = new PositionalSoundPlayer(AudioPlayer, PATH_TO_SOUNDS);

        gameManager = this.GetNode<GameManager>(Master.NODE_PATH_TO_GAME_MANAGER);
        this.Connect("PlayerDied", gameManager, "OnPlayerDied");
    }
    protected override void InitState()
    {
        this.currentState = new Idle(this, this.RotationDegrees);
    }

    public override void _Process(float delta)
    {
        currentState = ((PlayerState)currentState).HandleInput(delta);

        // TODO 9 lock sprites via hack code
        //this.GetNode<Sprite>("MoveSprite").RotationDegrees = 0;
        //this.GetNode<Sprite>("AttackSprite").RotationDegrees = 0;
    }
    public override void _PhysicsProcess(float delta)
	{
		currentState = (PlayerState)currentState.Update(delta);
	}

    public void ApplyIncomingAttack(Node2D attacker, Attack attack)
    {
        if (!Invulnerable && !(attack.Owner is Player))
        {
            currentState = currentState.SwitchState(new TakingDamage(this));
            int damage = attack.Damage - this.CurrentStats.Defense;
            if (gameManager.AlarmTriggered)
            {
                this.TakeDamage(damage);
            }
            else
            {
                gameManager.AdjustAlarmCountdown(-damage);
            }
            if (attack.PushForce > 0)
            {
                Vector2 hitDirection = (this.GlobalPosition - attacker.GlobalPosition).Normalized();
                this.incomingAttackForce = hitDirection * attack.PushForce;
            }
        }
    }
    public void TakeDamage(int hpAmount)
    {
        if (this.CurrentStats.ApplyDamage(hpAmount))
        {
            // When hp is depleted
            currentState = currentState.SwitchState(new Dying(this));
        }
    }

    public override void Die()
    {
        EmitSignal(nameof(PlayerDied), this);
        this.QueueFree();
    }

    public void ObtainItem(Item item)
    {
        SoundPlayer.Play("pickup_item");
        item.PickUp();
        if (item is CountDownModifier countDownModifier)
        {
            // Add time item value as money
            gameManager.AdjustMoney((int)countDownModifier.CountDownChange);
        }
    }

    public void AddToInventory(Item item)
    {
        throw new System.NotImplementedException();
    }

    public void DropItem(Item item)
    {
        throw new System.NotImplementedException();
    }

    public bool IsInventoryFull()
    {
        throw new System.NotImplementedException();
    }
}
