using code.Items;
using Godot;

public class ItemBox : Area2D
{
    public Item Parent { get; private set; }

    public override void _Ready()
    {
        Parent = this.GetParentOrNull<Item>();
        if (Parent == null) GD.PushWarning("ITEMBOX AT " + this.GlobalPosition + " COULD NOT GET PARENT THAT IS AN ITEM");
    }

    public void OnItemBoxAreaEntered(GrabberBox grabberBox)
    {
        grabberBox.GrabItem(Parent);
    }
}
