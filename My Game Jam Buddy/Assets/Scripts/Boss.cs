using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField] EvilGameManager evil;

    [Header("Health")]
    float maxHealth = 1000f;
    [SerializeField] float hp;
    public Slider healthSlider;

    [Header("Movement")]
    public float speed = 2f;
    public float horizontalRange = 3f;
    public float verticalBobAmount = 0.5f;

    [Header("Lifetime")]
    public float maxAliveTime = 30f;
    private float aliveTimer;

    [Header("UI References")]
    public TextMeshProUGUI timerText;

    [SerializeField] SpriteRenderer spriteRenderer;
    private bool isFlashing = false;
    private Vector3 startPosition;
    private float time;

    void Start()
    {
        hp = maxHealth;
        startPosition = transform.position;
        evil = GameObject.Find("Evil Game Manager").GetComponent<EvilGameManager>();
        healthSlider = GameObject.Find("BossHealthSlider").GetComponent<Slider>();
        timerText = GameObject.Find("BossTimerText").GetComponent<TextMeshProUGUI>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (healthSlider != null)
        {
            healthSlider.maxValue = hp;
            healthSlider.value = hp;
        }

        

        else
        {
            Debug.LogWarning("Health slider belum di-assign ke boss!");
        }
    }
    
    void Update()
    {
        time += Time.deltaTime;
        aliveTimer += Time.deltaTime;

        float xOffset = Mathf.Sin(time * speed) * horizontalRange;
        float yOffset = Mathf.Cos(time * speed * 1.5f) * verticalBobAmount;
        transform.position = startPosition + new Vector3(xOffset, yOffset, 0);

        if (aliveTimer >= maxAliveTime)
        {
            Die(); // kalau waktunya habis
        }

        float timeLeft = Mathf.Max(0, maxAliveTime - aliveTimer);
        if (timerText != null)
        {
            timerText.text = "Time Left: " + timeLeft.ToString("F1") + "s";
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Test");

        if (other.CompareTag("Player"))
        {
            hp -= 10f;
            Debug.Log("Boss HP: " + hp);

            if (healthSlider != null)
            {
                healthSlider.value = hp;
            }

            if (spriteRenderer != null && !isFlashing)
            {
                StartCoroutine(FlashColor(Color.red, 0.5f));
            }

            if (hp <= 0)
            {
                Die();
            }
        }
    }
    IEnumerator FlashColor(Color flashColor, float duration)
    {
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = Color.white;
    }

    void Die()
    {
        Debug.Log("Boss defeated!");
        evil.mouseCollider.enabled = false;
        if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(false);
        }
        healthSlider.value = maxHealth;
        timerText.text = "Good Luck!";
        AudioManager.Instance.PlayBGM("Main");
        Destroy(gameObject);
    }
}
