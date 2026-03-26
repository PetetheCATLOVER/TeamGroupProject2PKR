using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    
    public float scrollSpeed = 12f;

    void Update()
    {
        transform.Translate(Vector2.right * scrollSpeed * Time.deltaTime);
    }
}