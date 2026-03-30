using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float speed = 12f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (transform.position.x > 20f)
        {
            Destroy(gameObject);
        }
    }
}
