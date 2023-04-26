using Godot;
using System.Net;

public class ShopManager : Control
{
    private GameManager gameManager;
    private Player shopper;
    private AppendableRichTextLabel moneyLeft;
    private GridContainer items;

    [Signal]
    public delegate void ExitShop(Player player);

    public override void _Ready()
    {
        gameManager = this.GetNodeOrNull<GameManager>(Master.NODE_PATH_TO_GAME_MANAGER);
        moneyLeft = this.GetNode<AppendableRichTextLabel>("MoneyLeft");
        items = this.GetNode<GridContainer>("GridContainer");

        shopper = gameManager.Player;
        

        this.Connect("ExitShop", this.GetNode<Master>(Master.NODE_PATH_TO_MASTER), "OnExitShop");

        DisableItemsThatCantBePurchase();
    }
    public override void _Process(float delta)
    {
        moneyLeft.AppendToInitialText(gameManager.Money.ToString());
    }

    private void DisableItemsThatCantBePurchase()
    {
        foreach (ShopButton shopButton in items.GetChildren())
        {
            if (gameManager.Money < shopButton.currentPrice)
            {
                shopButton.Disabled= true;
            }
            else
            {
                shopButton.Disabled= false;
            }
        }
    }

    // **************************************************
    // Signal listeners
    // **************************************************
    public void OnExitButtonPressed()
    {
        EmitSignal(nameof(ExitShop), gameManager.Player);
    }
    public void OnBuyItem(string itemName, int currentPrice) 
    {
        gameManager.Money -= currentPrice; // First subtrack from money
        if (itemName.Equals("RESTORE HP"))
        {
            //gameManager.Money -= currentPrice;
            shopper.CurrentStats.Hp = shopper.CurrentStats.MaxHp;
        }
        else if (itemName.Equals("MORE TIME"))
        {
            gameManager.AdjustAlarmCountdown(3);
        }
        else if (itemName.Equals("RESTORE MP"))
        {
            shopper.CurrentStats.Mp = shopper.CurrentStats.MaxMp;
        }
        else if (itemName.Equals("RESTORE ALL"))
        {
            shopper.CurrentStats.Hp = shopper.CurrentStats.MaxHp;
            shopper.CurrentStats.Mp = shopper.CurrentStats.MaxMp;
        }
        else if (itemName.Equals("MAX HP UP"))
        {
            shopper.CurrentStats.MaxHp += 1;
            shopper.CurrentStats.Hp += 1;
        }
        else if (itemName.Equals("ATTACK UP"))
        {
            shopper.CurrentStats.Strength += 1;
        }
        else if (itemName.Equals("SPEED UP"))
        {
            shopper.CurrentStats.Speed += .2f;
        }

        DisableItemsThatCantBePurchase();
    }
}
