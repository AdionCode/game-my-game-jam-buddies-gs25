using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour
{
    [Header("Bug Hunter Task")]
    public GameObject bugHunterPanel;
    public List<GameObject> bugButtons;
    public int bugToActivate = 3;

    private int bugsFixed = 0;
    private bool gameDone = false;

    private void Start()
    {

    }

    public void StartTask()
    {
        StartCoroutine(BugHunter());
    }
    public bool IsTaskDone()
    {
        return gameDone;
    }

    #region Bug Hunter
    IEnumerator BugHunter()
    {
        bugHunterPanel.SetActive(true);
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
        bugHunterPanel.SetActive(false);
        gameDone = true;
    }

    public void OnBugClicked(GameObject bug)
    {
        bug.SetActive(false);
        bugsFixed++;

        if (bugsFixed >= bugToActivate)
            gameDone = true;
    }
    #endregion

}
