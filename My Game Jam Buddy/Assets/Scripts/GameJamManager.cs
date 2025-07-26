using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameJamManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameJamText;

    [SerializeField] private List<GameJamData> gameJams;
    private int selectedIndex = 0;

    public bool isWorking = false;
    private bool isMiniGameRunning = false;

    [SerializeField] IdleTimer IdleTimer;
    [SerializeField] StudioProgress studio;
    [SerializeField] CompanionManager companion;
    [SerializeField] TaskManager taskManager;

    private void Start()
    {
        IdleTimer.onTimerFinished.AddListener(OnTimerEnded);
        DisplaySelectedGameJam();
    }

    private void DisplaySelectedGameJam()
    {
        var selectedJam = gameJams[selectedIndex];
        Debug.Log("Dipilih: " + selectedJam.jamName + " (" + selectedJam.durationInSeconds + " detik)");
        gameJamText.text = $"{selectedJam.jamName}";
    }

    public void LeftSelect()
    {
        selectedIndex = Mathf.Max(0, selectedIndex - 1);
        DisplaySelectedGameJam();
    }

    public void RightSelect()
    {
        selectedIndex = Mathf.Min(gameJams.Count - 1, selectedIndex + 1);
        DisplaySelectedGameJam();
    }

    public void ChooseGameJam()
    {
        var selectedJam = gameJams[selectedIndex];
        IdleTimer.StartCountdown(selectedJam.durationInSeconds);
        companion.StartAllWorking();

        isWorking = true;
        StartCoroutine(RandomMiniGameTrigger());

        Debug.Log("Memulai Game Jam: " + selectedJam.jamName);
    }

    private void OnTimerEnded()
    {
        isWorking = false;
        StopAllCoroutines();
        Debug.Log("GameJamManager tahu: Timer selesai!");
        OnGameJamFinished();
    }

    void OnGameJamFinished()
    {
        companion.SetIdle();
        var selectedJam = gameJams[selectedIndex];
        int expEarned = Random.Range(selectedJam.minExp, selectedJam.maxExp + 1);
        studio.AddXP(expEarned);

        if (TryWinGameJam(studio.currentLevel))
        {
            int moneyEarned = selectedJam.Money;
            studio.AddMoney(moneyEarned);
            Debug.Log("Menang Game Jam! Dapat uang: " + moneyEarned);
        }
        else
        {
            Debug.Log("Tidak menang Game Jam. Tapi dapat EXP.");
        }
    }
    bool TryWinGameJam(int studioLevel)
    {
        float winChance = Mathf.Clamp01(0.2f + (studioLevel * 0.05f));
        return Random.value < winChance;
    }

    private IEnumerator RandomMiniGameTrigger()
    {
        while (isWorking)
        {
            float delay = Random.Range(10f, 30f);
            yield return new WaitForSeconds(delay);

            if (!isWorking || isMiniGameRunning) continue;

            // Jalankan minigame
            isMiniGameRunning = true;
            taskManager.StartTask();

            // Tunggu hingga selesai
            while (!taskManager.IsTaskDone())
                yield return null;

            isMiniGameRunning = false;
        }
    }
}
