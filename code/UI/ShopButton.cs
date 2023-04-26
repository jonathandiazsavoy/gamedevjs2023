using code.Items;
using Godot;

public class ShopButton : Button
{
    [Export]
    public StreamTexture ItemImage { get; set; }
    [Export]
    public int BasePrice { get; set; }
    [Export]
    public string ItemName { get; set; }
    [Export]
    public bool PriceIncreasesWithNumberBought { get; set; }

    public int currentPrice;

    private ShopManager shopManager;

    [Signal]
    public delegate void BuyItem(string itemName, int currentPrice);

    public override void _Ready()
    {
        shopManager = this.GetNode<ShopManager>(Master.NODE_PATH_TO_SHOP_SCREEN+"/Shop");
        this.Connect("BuyItem", shopManager, "OnBuyItem");
        currentPrice = BasePrice;

        this.GetNode<TextureRect>("MarginContainer/HBoxContainer/TextureRect").Texture=ItemImage;
        this.GetNode<Label>("MarginContainer/HBoxContainer/VBoxContainer/ItemName").Text = ItemName;
        this.GetNode<Label>("MarginContainer/HBoxContainer/VBoxContainer/ItemCost").Text = currentPrice.ToString();
    }

    public void OnButtonPressed()
    {
        EmitSignal("BuyItem", ItemName, currentPrice);
    }
}
