using UnityEngine;

public class ResizeWindow : MonoBehaviour
{
    [SerializeField] private RectTransform Window;
    private Vector3 initialScale = new Vector3(1f, 1f, 1f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeWindow()
    {
        Vector3 currentScale = Window.transform.localScale;

        // Tambah 25%
        currentScale *= 1.25f;

        if (currentScale.x >= 2f || currentScale.y >= 2f || currentScale.z >= 2f)
        {
            currentScale = initialScale;
        }

        Window.transform.localScale = currentScale;
    }
}
