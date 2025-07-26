using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompanionManager : MonoBehaviour
{
    [SerializeField] private List<CompanionBehaviour> companions;

    [Header("Companion UI")]
    [SerializeField] TextMeshProUGUI companionNameText;

    private void Start()
    {
        SetIdle();
        companionNameText.text = "locked";
    }

    public void SetIdle()
    {
        foreach (var c in companions)
        {
            if (c.isUnlocked)
                c.PlayIdleAnimation();
        }
    }

    public void UnlockByLevel(int level)
    {
        if (level == 2)
        {
            Debug.Log("Unlocking Companion 1");
            companionNameText.text = "Unlocked";
            companions[1].Unlock();
        }
        
    }

    public void StartAllWorking()
    {
        foreach (var c in companions)
        {
            if (c.isUnlocked)
                c.PlayWorkingAnimation();
        }
    }

    
    public void ChangeTextUnlock()
    {

    }
}
