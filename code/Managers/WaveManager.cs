using code.Helpers;
using Godot;

public class WaveManager : YSort
{
    private GameManager gameManager;
    private YSort Enemies;
    private YSort Items;

    public int EnemyCount { get; private set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        gameManager = this.GetNode<GameManager>("/root/GameManager");
        Enemies = this.GetNode<YSort>("Enemies");
        Items = this.GetNode<YSort>("Items");
        EnemyCount = Enemies.GetChildCount();
    }

    // **************************************************
    // Signal listeners
    // **************************************************
    public void OnAlarmCountdownTimeout()
    {
        Items.QueueFree();
    }
}
