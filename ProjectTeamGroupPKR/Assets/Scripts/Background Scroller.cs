using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public Transform bg1;
    public Transform bg2;

    public float scrollSpeed = 12f;
    public float backgroundWidth = 20f;

    private bool isMoving = false;

    void Update()
    {
        if (!isMoving) return;

        bg1.Translate(Vector2.right * scrollSpeed * Time.deltaTime);
        bg2.Translate(Vector2.right * scrollSpeed * Time.deltaTime);

        if (bg1.position.x >= backgroundWidth)
        {
            Reposition(bg1, bg2);
        }

        if (bg2.position.x >= backgroundWidth)
        {
            Reposition(bg2, bg1);
        }
    }

    void Reposition(Transform current, Transform other)
    {
        current.position = new Vector3(
            other.position.x - backgroundWidth,
            current.position.y,
            current.position.z
        );
    }

    public void StartScrolling()
    {
        isMoving = true;
    }
}
