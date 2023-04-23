using Godot;

public class ShopManager : Control
{
    private GameManager gameManager;
    private Player shopper;
    private AppendableRichTextLabel moneyLeft;

    [Signal]
    public delegate void ExitShop(Player player);

    public override void _Ready()
    {
        gameManager = this.GetNode<GameManager>(Master.NODE_PATH_TO_GAME_MANAGER);
        shopper = gameManager.Player;
        moneyLeft = this.GetNode<AppendableRichTextLabel>("MoneyLeft");

        moneyLeft.AppendToInitialText(gameManager.Money.ToString());

        this.Connect("ExitShop", this.GetNode<Master>(Master.NODE_PATH_TO_MASTER), "OnExitShop");
    }

    // **************************************************
    // Signal listeners
    // **************************************************
    public void OnExitButtonPressed()
    {
        EmitSignal(nameof(ExitShop), gameManager.Player);
    }
}
