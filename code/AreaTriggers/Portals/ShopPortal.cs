using Godot;

public class ShopPortal : Area2D
{
    private Node master;

    [Signal]
    public delegate void GoToShop(Player player);

    public override void _Ready()
    {
        master = this.GetNode<Node>(Master.NODE_PATH_TO_MASTER);
        this.Connect("GoToShop", master, "OnGoToShop");
    }

    public void OnPortalEntered(Node body)
    {
        if (body is Player player) EmitSignal(nameof(GoToShop), player);
    }
}
