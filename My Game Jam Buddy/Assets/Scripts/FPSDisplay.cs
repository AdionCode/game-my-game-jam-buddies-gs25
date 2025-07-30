using UnityEngine;
using TMPro; // ganti ke UnityEngine.UI kalau pakai UI biasa

public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText; // ganti ke Text jika pakai UI biasa
    float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString() + " FPS";
    }
}