using code.Helpers;
using code.Items;
using code.StateMachines.CharacterStates.PlayerStates;
using Godot;

public class Player : Character, IObtainer
{
	public override void _Ready()
	{
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

    public void ObtainItem(Item item)
    {
        SoundPlayer.Play("pickup item");
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
