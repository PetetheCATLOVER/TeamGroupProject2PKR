using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float speed = 12f;

    void Update()
    {
        // ?? MOVE LEFT (same illusion as bullets)
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // cleanup when off screen
    }
}
