namespace code.Items
{
    public class InstantUseItem : Item
    {
        public override void Drop()
        {
            return;
        }

        public override void PickUp()
        {
            this.Use();
            this.QueueFree();
        }
    }
}
