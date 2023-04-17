using code.Items;
using Godot;

public class Player : Character, IObtainer
{
	private new PlayerState currentState;

	public override void _Ready()
	{
        this.InitNodes();

		this.BaseStats = new CharacterStats(3, 0, 1, 0, 1.5f);
		this.CurrentStats = this.BaseStats;

		
        currentState = new IdleState(this);
		currentAttack = new Attack(1, 100);
	}

	public override void _PhysicsProcess(float delta)
	{
		currentState = (PlayerState)currentState.HandleInputAndUpdate(delta);
	}

    public void ObtainItem(Item item)
    {
        item.PickUp();
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
