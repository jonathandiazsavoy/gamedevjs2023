using Godot;

public class WaveManager : YSort
{
    private YSort Enemies;

    public int EnemyCount { get; private set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Enemies = this.GetNode<YSort>("Enemies");
        EnemyCount = Enemies.GetChildCount();
    }
}
