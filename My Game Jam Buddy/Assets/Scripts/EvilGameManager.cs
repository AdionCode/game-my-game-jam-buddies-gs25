using UnityEngine;
using System.Collections;

public class EvilGameManager : MonoBehaviour
{
    [SerializeField] GameObject bossUIPanel;
    [SerializeField] GameObject bossPrefab;
    [SerializeField] Transform bossSpawnPoint;
    public Collider2D mouseCollider;

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Berhenti play mode jika di Editor
#else
                Application.Quit(); // Keluar dari aplikasi jika sudah di-build
#endif
    }

    public void StartBossBattle()
    {
        StartCoroutine(StartBossBattleSequence());
    }

    IEnumerator StartBossBattleSequence()
    {
        mouseCollider.enabled = true;
        AudioManager.Instance.PlayBGM("Boss");
        // 1. Tampilkan UI
        bossUIPanel.SetActive(true);

        // 2. Tunggu sebentar (misalnya 2 detik)
        yield return new WaitForSeconds(2f);

        // 3. Munculkan boss
        Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
    }
}