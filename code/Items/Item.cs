using Godot;

namespace code.Items
{
    public abstract class Item : Node
    {
        [Signal]
        public delegate void ItemUsed(Item item);

        public abstract void PickUp();
        public abstract void Drop();
        public void Use()
        {
            EmitSignal(nameof(ItemUsed), this);
        }
    }
}
