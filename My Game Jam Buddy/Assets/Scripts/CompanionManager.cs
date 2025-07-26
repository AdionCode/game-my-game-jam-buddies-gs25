using System.Collections.Generic;
using UnityEngine;

public class CompanionManager : MonoBehaviour
{
    [SerializeField] private List<CompanionBehaviour> companions;

    private void Start()
    {
        SetIdle();
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
            Debug.Log("Unlocking Companion 1");
            companions[1].Unlock();
        if (level == 4)
            companions[2].Unlock();
    }

    public void StartAllWorking()
    {
        foreach (var c in companions)
        {
            if (c.isUnlocked)
                c.PlayWorkingAnimation();
        }
    }
}
