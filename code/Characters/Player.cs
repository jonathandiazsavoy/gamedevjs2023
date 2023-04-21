using code.Helpers;
using code.Items;
using code.Items.TimeItems;
using code.StateMachines.CharacterStates.PlayerStates;

public class Player : Character, IObtainer
{
    private GameManager gameManager;

	public override void _Ready()
	{
        base.InitNodes();
        this.InitNodes();

		this.BaseStats = new CharacterStats(3, 0, 1, 0, 1.5f);
		this.CurrentStats = this.BaseStats;

		
        currentState = new Idle(this);
		currentAttack = new Attack(1, 100);
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
