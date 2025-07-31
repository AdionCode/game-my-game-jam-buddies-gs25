using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public int trailIndex;
    public Color trailColor;
    public int price = 100;

    public TrailRenderer mouseTrail;
    public Button button;
    public TextMeshProUGUI label;

    private static bool[] trailUnlocked;
    private static int? currentTrailIndex = null;
    private static int playerMoney = 300; // nanti kamu bisa ganti ini ke GameManager

    [SerializeField] private List<ShopItemData> item;

    void Start()
    {
        if (trailUnlocked == null || trailUnlocked.Length == 0)
        {
            trailUnlocked = new bool[10];
        }

        button.onClick.AddListener(OnButtonClick);
        UpdateButtonText();
    }

    void OnButtonClick()
    {
        if (!trailUnlocked[trailIndex])
        {
            if (playerMoney >= price)
            {
                playerMoney -= price;
                trailUnlocked[trailIndex] = true;
                Debug.Log("Bought trail " + trailIndex + ". Money left: " + playerMoney);
            }
            else
            {
                Debug.Log("Not enough money!");
                return;
            }
        }
        else if (currentTrailIndex == trailIndex)
        {
            // Unequip
            mouseTrail.enabled = false;
            currentTrailIndex = null;
        }
        else
        {
            // Equip
            mouseTrail.startColor = trailColor;
            mouseTrail.endColor = trailColor;
            mouseTrail.enabled = true;
            currentTrailIndex = trailIndex;
        }

        UpdateButtonText();
    }

    void UpdateButtonText()
    {
        if (!trailUnlocked[trailIndex])
        {
            label.text = "Buy (" + price + ")";
        }
        else if (currentTrailIndex == trailIndex)
        {
            label.text = "Unequip";
        }
        else
        {
            label.text = "Equip";
        }
    }
}
