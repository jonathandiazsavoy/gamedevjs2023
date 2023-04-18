using code.StateMachines.CharacterStates.NPCStates.EnemyStates;
using Godot;

public class Enemy : Character
{
    [Signal]
    public delegate void EnemyDied(Enemy enemy);

    public override void _Ready()
    {
        this.InitNodes();

        this.BaseStats = new CharacterStats(3, 0, 1, 0, 1.5f);
        this.CurrentStats = this.BaseStats;


        currentState = new Idle(this);
        currentAttack = new Attack(1, 100);
    }

    public override void _PhysicsProcess(float delta)
    {
        currentState = (EnemyState)currentState.Update(delta);
    }

    public void Die()
    {
        EmitSignal(nameof(EnemyDied), this);
        this.QueueFree();
    }
}
