using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class GameJamManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameJamText;
    [SerializeField] private TextMeshProUGUI gameJamLogHistory;

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
        float baseChance = Mathf.Clamp01(0.2f + (studio.currentLevel * 0.1f));
        float winChance = baseChance * (1f - selectedJam.difficulty);

        int minutes = Mathf.RoundToInt(selectedJam.durationInSeconds / 60f);

        Debug.Log("Dipilih: " + selectedJam.jamName + " (" + selectedJam.durationInSeconds + " detik)");
        gameJamText.text =
            $"[ {selectedJam.jamName} ]\n" +
            $"> Duration -> {minutes} min\n" +
            $"> EXP Gain -> +{selectedJam.Exp}\n" +
            $"> Win Reward -> +{selectedJam.Money} c\n" +
            $"> Chance : [{winChance * 100f:0}%]";
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
        DisplaySelectedGameJam();
        Debug.Log("GameJamManager tahu: Timer selesai!");
        OnGameJamFinished();
    }

    void OnGameJamFinished()
    {
        companion.SetIdle();
        var selectedJam = gameJams[selectedIndex];
        studio.AddXP(selectedJam.Exp);
        int moneyEarned = selectedJam.Money;

        if (TryWinGameJam(studio.currentLevel))
        {
            studio.AddMoney(moneyEarned);
            gameJamLogHistory.text = $"[ {selectedJam.jamName} ]  \r\n> Result -> Win! \r\n> Coin -> (+{moneyEarned})\r\n> Exp -> (+{selectedJam.Exp})";
            Debug.Log("Menang Game Jam! Dapat uang: " + moneyEarned);
        }
        else
        {
            gameJamLogHistory.text = $"[ {selectedJam.jamName} ]  \r\n> Result -> Lose! \r\n> Coin -> (+0)\r\n> Exp -> (+{selectedJam.Exp})";
            Debug.Log("Tidak menang Game Jam. Tapi dapat EXP.");
        }
    }
    bool TryWinGameJam(int studioLevel)
    {
        var selectedJam = gameJams[selectedIndex];
        float baseChance = Mathf.Clamp01(0.2f + (studioLevel * 0.1f));
        float winChance = baseChance * (1f - selectedJam.difficulty);
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
