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

    private Item item;
    private int currentPrice;

    public override void _Ready()
    {
        
    }
}
