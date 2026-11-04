using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            Destroy(gameObject);
            LevelManager.instance.Respawn();
        }
    }
}
