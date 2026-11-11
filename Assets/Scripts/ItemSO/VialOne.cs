using UnityEngine;
using System;

public class VialOne : MonoBehaviour
{
   public ItemSO itemSO;
   public SpriteRenderer sr;
   public Animator anim;

   public int quantity;
   public static event Action<ItemSO, int> OnItemLooted;


   private void OnValidate()
   {
	   if (itemSO == null)
	       return;
	   sr.sprite = itemSO.icon;
	   this.name = itemSO.itemName;
   }

   private void OnTriggerEnter2D(Collider2D collision)
   {
	   if (collision.CompareTag("Player"))
	   {
		   anim.Play("VialPickup");
			OnItemLooted?.Invoke(itemSO, quantity);
		   Destroy(gameObject, .5f);
	   }
   }
}
