using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameJamManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameJamText;
    [SerializeField] private IdleTimer IdleTimer;

    [SerializeField] private List<GameJamData> gameJams;
    private int selectedIndex = 0;

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
        Debug.Log("Left Select Button Pressed");
        selectedIndex = Mathf.Max(0, selectedIndex - 1);
        DisplaySelectedGameJam();
    }

    public void RightSelect()
    {
        Debug.Log("Right Select Button Pressed");
        selectedIndex = Mathf.Min(gameJams.Count - 1, selectedIndex + 1);
        DisplaySelectedGameJam();
    }

    private void ChangeGameJam()
    {
        gameJamText.text = "Game Jam Changed!";
    }

    public void ChooseGameJam()
    {
        var selectedJam = gameJams[selectedIndex];
        IdleTimer.StartCountdown(selectedJam.durationInSeconds);
        Debug.Log("Memulai Game Jam: " + selectedJam.jamName);
    }

    private void OnTimerEnded()
    {
        Debug.Log("GameJamManager tahu: Timer selesai!");
        // Tambahkan aksi di sini: tampilkan UI selesai, companion dapat exp, dll
    }
}
