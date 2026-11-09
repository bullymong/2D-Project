using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            LevelManager.instance.Respawn();
        }
        if (collision.gameObject.CompareTag("Hazard2"))
        {
            LevelManager.instance.Respawn2();
        }
        if (collision.gameObject.CompareTag("Hazard3"))
        {
            LevelManager.instance.Respawn3();
        }
    }
}
