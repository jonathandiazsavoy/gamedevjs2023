using Godot;

public class Player : Character
{
	private new PlayerState currentState;

	public override void _Ready()
	{
        this.InitNodes();

		this.BaseStats = new CharacterStats(3, 0, 1, 0, 5);
		this.CurrentStats = this.BaseStats;

		
        currentState = new IdleState(this);
		currentAttack = new Attack(1, 100);
	}

	public override void _PhysicsProcess(float delta)
	{
		currentState = (PlayerState)currentState.HandleInputAndUpdate(delta);
	}
}
