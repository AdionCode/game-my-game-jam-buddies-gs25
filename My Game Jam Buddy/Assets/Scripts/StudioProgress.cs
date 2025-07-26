using UnityEngine;

public class StudioProgress : MonoBehaviour
{
    [SerializeField] CompanionManager companion;

    public int currentLevel;
    public int currentXP;
    public int xpToNextLevel;
    public int money;

    public void AddXP(int amount)
    {
        currentXP += amount;
        while (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
        // Update UI atau apapun
    }

    private void LevelUp()
    {
        currentLevel++;
        currentXP -= xpToNextLevel;
        xpToNextLevel += 100; 

        companion.UnlockByLevel(currentLevel);
    }
}
