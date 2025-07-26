using UnityEngine;

public class CompanionBehaviour : MonoBehaviour
{
    public string companionName;
    public bool isUnlocked;
    [SerializeField] GameObject visual;

    [SerializeField] private Animator animator;

    public void Unlock()
    {
        isUnlocked = true;
        visual.SetActive(true);
        PlayIdleAnimation();
    }

    public void PlayIdleAnimation()
    {
        animator?.Play("Idle");
    }

    public void PlayWorkingAnimation()
    {
        animator?.Play("Working");
    }

    public void PlayCelebrateAnimation()
    {
        animator?.Play("Celebrate");
    }
}
