using Godot;

public class ShopPortal : Area2D
{
    private Node master;

    [Signal]
    public delegate void ShopEntered(Player player);

    public override void _Ready()
    {
        master = this.GetNode<Node>(Master.NODE_PATH_TO_MASTER);
        this.Connect("ShopEntered", master, "OnShopEntered");
    }

    public void OnPortalEntered(Node body)
    {
        if (body is Player player) EmitSignal(nameof(ShopEntered), player);
    }
}
