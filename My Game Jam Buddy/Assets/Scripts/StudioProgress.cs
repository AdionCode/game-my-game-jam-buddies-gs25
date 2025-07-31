using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StudioProgress : MonoBehaviour
{
    [SerializeField] CompanionManager companion;

    [SerializeField] private Slider xpSlider;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI levelDetailText;
    [SerializeField] private TextMeshProUGUI moneyText;

    public int currentLevel;
    public int currentXP;
    public int xpToNextLevel;
    public int money;

    private void Start()
    {
        UpdateUI();
    }

    public void AddXP(int amount)
    {
        currentXP += amount;
        while (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }

        UpdateUI();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateUI();
    }

    private void LevelUp()
    {
        currentLevel++;
        currentXP -= xpToNextLevel;
        xpToNextLevel += 100; 

        companion.UnlockByLevel(currentLevel);
    }

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            UpdateUI();
            return true;
        }
        else
        {
            AudioManager.Instance.PlaySFX("Error");
            Debug.Log("Not enough money");
            return false;
        }
    }

    private void UpdateUI()
    {
        if (xpSlider != null)
        {
            xpSlider.maxValue = xpToNextLevel;
            xpSlider.value = currentXP;
        }

        if (levelText != null)
        {
            levelText.text = "Studio Level " + currentLevel;
        }

        if (levelDetailText != null)
        {
            levelDetailText.text = $"[{(currentXP / (float)xpToNextLevel) * 100f:0.00}%] {currentXP}/{xpToNextLevel}";
        }

        if (moneyText != null)
        {
            moneyText.text = "Coins : " + money;
        }
    }
}
