using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void ToggleWindow(GameObject window)
    {
        if (window != null)
            window.SetActive(!window.activeSelf);
        AudioManager.Instance.PlaySFX("Select");
    }

    public void ShowWindow(GameObject window)
    {
        if (window != null)
            window.SetActive(true);
    }

    public void HideWindow(GameObject window)
    {
        if (window != null)
            window.SetActive(false);
    }
}
