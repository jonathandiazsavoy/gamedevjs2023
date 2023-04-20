using code.Items;
using Godot;

public class GrabberBox : Area2D
{
    private IObtainer parent;

    public override void _Ready()
    {
        parent = this.GetParentOrNull<IObtainer>();
        if (parent == null) GD.PushWarning("GRABBERBOX AT " + this.GlobalPosition + " COULD NOT GET PARENT THAT IS AN IOBTAINER");
    }

    public void GrabItem(Item item)
    {
        parent.ObtainItem(item);
    }
}
