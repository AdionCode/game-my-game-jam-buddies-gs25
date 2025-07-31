using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class IdleTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI timerText2;

    private float timeRemaining;
    private bool timerRunning = false;

    public UnityEvent onTimerFinished;

    public void StartCountdown(float duration)
    {
        timeRemaining = duration;
        timerRunning = true;
    }

    void Update()
    {
        if (!timerRunning) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            timeRemaining = 0;
            timerRunning = false;
            onTimerFinished.Invoke();
            timerText.text = "Select";
            timerText2.text = "Not in Game Jam";
            Debug.Log("Timer selesai");
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText2.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
