using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Transform respawnPoint;
    public Transform respawnPoint2;
    public Transform respawnPoint3;
    public Transform player;

    private void Awake()
    {
        instance = this;
    }

    public void Respawn()
    {
       player.position = respawnPoint.position;
    }
     public void Respawn2()
    {
       player.position = respawnPoint2.position;
    }
    public void Respawn3()
    {
       player.position = respawnPoint3.position;
    }
}
