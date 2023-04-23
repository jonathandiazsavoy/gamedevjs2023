using code.Items;
using Godot;
using System;

public class ShopButton : Button
{
    [Export]
    public Item Item { get; set; }
    [Export]
    public int BasePrice { get; set; }
    [Export]
    public bool PriceIncreasesWithNumberBought { get; set; }

    private Item item;
    private int currentPrice;

    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
