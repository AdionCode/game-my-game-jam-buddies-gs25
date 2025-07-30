using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // pastikan z = 0 di 2D
        transform.position = mousePosition;
    }
}
