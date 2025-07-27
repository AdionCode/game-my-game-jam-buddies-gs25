using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class TaskManager : MonoBehaviour
{
    [SerializeField] StudioProgress studio;
    public GameObject minigamePanel;
    private bool gameDone = false;

    [Header("Bug Hunter Task")]
    public List<GameObject> bugButtons;
    public int bugToActivate = 3;
    private int bugsFixed = 0;

    [Header("Are You a Robot? Task")]
    [SerializeField] GameObject capcthaObject;
    [SerializeField] TextMeshProUGUI verifyText;
    int left2press = 10;

    private void Start()
    {
        StartCoroutine(AreYouARobot());
    }

    public void StartTask()
    {
        StartCoroutine(BugHunter());
        StartCoroutine(AreYouARobot());
    }
    public bool IsTaskDone()
    {
        return gameDone;
    }

    #region Bug Hunter
    IEnumerator BugHunter()
    {
        minigamePanel.SetActive(true);
        bugsFixed = 0;
        gameDone = false;

        // Matikan semua dulu
        foreach (var b in bugButtons)
            b.SetActive(false);

        // Nyalakan bug acak sebanyak bugToActivate
        int activated = 0;
        while (activated < bugToActivate)
        {
            int randIndex = Random.Range(0, bugButtons.Count);
            if (!bugButtons[randIndex].activeSelf)
            {
                bugButtons[randIndex].SetActive(true);
                activated++;
            }
        }

        float timeout = 10f; // durasi maksimal minigame
        float timer = 0f;

        // Tunggu sampai selesai atau timeout
        while (!gameDone && timer < timeout)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // Cleanup
        foreach (var b in bugButtons)
            b.SetActive(false);
        minigamePanel.SetActive(false);
        gameDone = true;
    }

    public void OnBugClicked(GameObject bug)
    {
        bug.SetActive(false);
        bugsFixed++;

        if (bugsFixed >= bugToActivate)
        {
            studio.AddXP(25);
            gameDone = true;
        }
    }
    #endregion

    #region Are You a Robot?

    IEnumerator AreYouARobot()
    {
        gameDone = false;
        minigamePanel.SetActive(true);
        capcthaObject.SetActive(true);

        left2press = Random.Range(10, 30);

        float timeout = 10f;
        float timer = 0f;

        while (!gameDone && timer < timeout)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        capcthaObject.SetActive(false);
        minigamePanel.SetActive(false);
        gameDone = true;
    }

    public void VerifyButton()
    {
        verifyText.text = $"Yeah sure buddy.\r\nyou need to press {left2press} again.";
        left2press--;

        if (left2press == 0)
        {
            studio.AddXP(25);
            gameDone = true;
        }
    }

    #endregion

}
