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
    [SerializeField] GameObject bugHunterObject;
    public int bugToActivate = 3;
    private int bugsFixed = 0;

    [Header("Are You a Robot? Task")]
    [SerializeField] GameObject capcthaObject;
    [SerializeField] TextMeshProUGUI verifyText;
    int left2press = 10;

    [Header("Don't Click the Button Task")]
    [SerializeField] GameObject dontClickButtonObject;
    [SerializeField] TextMeshProUGUI dontClickButtonText;

    [Header("Stay Hydrated Task")]
    [SerializeField] GameObject stayHydratedObject;

    private void Start()
    {

    }

    public void StartTask()
    {
        minigamePanel.SetActive(true);
        gameDone = false;

        int pick = Random.Range(0, 4);
        switch (pick)
        {
            case 0:
                StartCoroutine(BugHunter());
                break;
            case 1:
                StartCoroutine(AreYouARobot());
                break;
            case 2:
                StartCoroutine(DontClickTheButton());
                break;
            case 3:
                StartCoroutine(StayHydrated());
                break;
        }
    }

    public void EndTask()
    {
        minigamePanel.SetActive(false);
        gameDone = true;
    }

    public bool IsTaskDone()
    {
        return gameDone;
    }

    #region Bug Hunter
    IEnumerator BugHunter()
    {
        bugHunterObject.SetActive(true);
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
        bugHunterObject.SetActive(false);
        EndTask();
    }

    public void OnBugClicked(GameObject bug)
    {
        bug.SetActive(false);
        bugsFixed++;

        if (bugsFixed >= bugToActivate)
        {
            studio.AddXP(25);
            EndTask();
        }
    }
    #endregion

    #region Are You a Robot?

    IEnumerator AreYouARobot()
    {
        gameDone = false;
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
        verifyText.text = "";
        EndTask();
    }

    public void VerifyButton()
    {
        verifyText.text = $"Yeah sure buddy.\r\nyou need to press {left2press} again.";
        left2press--;

        if (left2press == 0)
        {
            studio.AddXP(25);
            verifyText.text = "";
            gameDone = true;
        }
    }

    #endregion

    #region Don't Click the Button

    IEnumerator DontClickTheButton()
    {
        dontClickButtonObject.SetActive(true);

        float timeout = 10f;
        float timer = 0f;

        while (!gameDone && timer < timeout)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        dontClickButtonObject.SetActive(false);
        studio.AddXP(25);
        EndTask();
    }

    public void DontClickButton()
    {
        string[] reactionTexts = {
        "Bruh ._.",
        "You had one job...",
        "Bruh, really?",
        "NASA called. They said stop.",
        "You clicked it. Happy now?",
        "Error 404: self-control not found.",
        "Great, now we all die.",
        "This is why we can’t have nice things.",
        "I warned you, didn’t I?",
        "That's it, I'm telling mom."
    };

        int index = Random.Range(0, reactionTexts.Length);
        dontClickButtonText.text = reactionTexts[index];
    }

    #endregion

    #region Stay Hydrated

    IEnumerator StayHydrated()
    {
        stayHydratedObject.SetActive(true);

        float timeout = 10f;
        float timer = 0f;

        while (!gameDone && timer < timeout)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        stayHydratedObject.SetActive(false);
        EndTask();
    }

    public void StayHydratedButton()
    {
        studio.AddXP(25);
        EndTask();
    }

    #endregion

}
