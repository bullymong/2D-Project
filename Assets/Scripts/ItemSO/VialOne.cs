using UnityEngine;

public class VialOne : MonoBehaviour
{
   public ItemSO itemSO;
   public SpriteRenderer sr;
   public Animator anim;

   public int quantity;

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
		   Destroy(gameObject, .5f);
	   }
   }
}
