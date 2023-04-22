using Godot;

public class ShopPortal : Area2D
{
    private GameManager gameManager;

    [Signal]
    public delegate void GoToShop(Player player);

    public override void _Ready()
    {
        gameManager = this.GetNode<GameManager>(Master.NODE_PATH_TO_GAME_MANAGER);
        this.Connect("GoToShop", gameManager, "OnGoToShop");
    }

    public void OnPortalEntered(Node body)
    {
        if (body is Player player) EmitSignal(nameof(GoToShop), player);
    }
}
