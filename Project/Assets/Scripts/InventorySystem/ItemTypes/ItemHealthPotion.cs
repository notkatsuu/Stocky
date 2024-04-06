using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory System/Items/HealthPotion")]
public class ItemHealthPotion : ConsumableItem
{
    public override void Use(IConsume consumer)
    {
        consumer.Use(this);
    }
}