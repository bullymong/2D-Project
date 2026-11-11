using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventorySlot : MonoBehaviour
{
    public ItemSO itemSO;
    public int quantity;

    public Image itemImage;
    public TMP_Text quantityText;

    public void UpdateUI()
    {
        if (itemSO != null)
        {
            Debug.Log("Collected Item");
            itemImage.sprite = itemSO.icon;
            itemImage.gameObject.SetActive(true);
            quantityText.text = quantity.ToString();
        }
        else
        {
            Debug.Log("Not Collect Item");
            itemImage.gameObject.SetActive(false);
            quantityText.text = "";
        }
        
    }

}
