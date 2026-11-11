using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] itemSlots;
    private void OnEnable()
    {
        VialOne.OnItemLooted += AddItem;
    }
    private void OnDisable()
    {
        VialOne.OnItemLooted -= AddItem;
    }

    public void AddItem(ItemSO itemSO, int quantity)
    {
        foreach (var item in itemSlots)
        {
            if (item.itemSO == null)
            {
                item.itemSO = itemSO;
                item.quantity = quantity;
                item.UpdateUI();
                return;
            }
            
        }
    }
}
