using code.Items;
using Godot;

public class Wave : YSort
{
    public YSort Enemies { get; private set; }
    public YSort Items { get; private set; }

    public override void _Ready()
    {
        Enemies = this.GetNode<YSort>("Enemies");
        Items = this.GetNode<YSort>("Items");

        foreach (Enemy enemy in Enemies.GetChildren())
        {
            // Load enemies in as unalerted
            enemy.Alerted = false;
            enemy.Connect("EnemyDied", this.GetNode<GameManager>(Master.NODE_PATH_TO_GAME_MANAGER), "OnEnemyDied");
        }
        foreach (Item item in Items.GetChildren())
        {
            item.Connect("ItemUsed", this.GetNode<GameManager>(Master.NODE_PATH_TO_GAME_MANAGER), "OnCountDownModifierItemUsed");
        }
    }
}
