using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int totalCoins;
    public TMP_Text coinText;

    void Start()
    {
        coinText.text = "Coins: " + totalCoins;
    }

    public void ChangeCoins(int amount)
    {
        totalCoins += amount;
        coinText.text = "Coins: " + totalCoins;
    }
}
