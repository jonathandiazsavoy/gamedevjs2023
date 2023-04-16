using code.Items;

public interface IObtainer
{
    void ObtainItem(Item item);
    void AddToInventory(Item item);
    void DropItem(Item item);
    bool IsInventoryFull();
}
