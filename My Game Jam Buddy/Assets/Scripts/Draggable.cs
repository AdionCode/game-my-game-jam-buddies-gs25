using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private Camera uiCamera;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        uiCamera = canvas.worldCamera;

        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();

        }
        if (uiCamera == null)
        {
            uiCamera = Camera.main;
        }
    }

    private void Update()
    {
        ClampPosition();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("begin drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, eventData.position, uiCamera, out pos))
        {
            rectTransform.localPosition = pos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("end drag");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.SetAsLastSibling();
    }

    private void ClampPosition()
    {
        Vector3 pos = rectTransform.position;

        float height = 2f * uiCamera.orthographicSize;
        float width = height * uiCamera.aspect;

        float halfWidth = rectTransform.rect.width * 0.5f * rectTransform.lossyScale.x;
        float halfHeight = rectTransform.rect.height * 0.5f * rectTransform.lossyScale.y;

        float minX = uiCamera.transform.position.x - width / 2f + halfWidth;
        float maxX = uiCamera.transform.position.x + width / 2f - halfWidth;
        float minY = uiCamera.transform.position.y - height / 2f + halfHeight;
        float maxY = uiCamera.transform.position.y + height / 2f - halfHeight;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        rectTransform.position = pos;
    }
}
