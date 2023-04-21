using Godot;

public class ShopPortal : Area2D
{
    [Signal]
    public delegate void GoToShop(Player player);

    public void OnPortalEntered(Node body)
    {
        if (body is Player player) EmitSignal(nameof(GoToShop), player);
    }
}
