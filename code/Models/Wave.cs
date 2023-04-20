using Godot;

public class Wave : YSort
{
    public YSort Enemies { get; private set; }
    public YSort Items { get; private set; }

    public override void _Ready()
    {
        Enemies = this.GetNode<YSort>("Enemies");
        Items = this.GetNode<YSort>("Items");
    }
}
