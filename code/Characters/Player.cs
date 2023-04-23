using code.Items;
using code.Items.TimeItems;
using code.StateMachines.CharacterStates.PlayerStates;
using Godot;

public class Player : Character, IHurtable, IObtainer
{
    private GameManager gameManager;

	public override void _Ready()
	{
        base.InitNodes();
        this.InitNodes();

		this.BaseStats = new CharacterStats(3, 0, 1, 0, 1.5f);
		this.CurrentStats = this.BaseStats;

		
        currentState = new Idle(this);
		currentAttack = new Attack(1, 50);
	}

    public override void _Process(float delta)
    {
        currentState = ((PlayerState)currentState).HandleInput(delta);
    }
    public override void _PhysicsProcess(float delta)
	{
		currentState = (PlayerState)currentState.Update(delta);
	}

    protected override void InitNodes()
    {
        gameManager = this.GetNode<GameManager>(Master.NODE_PATH_TO_GAME_MANAGER);
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
                this.MoveAndSlide(hitDirection * attack.PushForce);
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

    public void ObtainItem(Item item)
    {
        SoundPlayer.Play("pickup item");
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
