using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Move LEFT toward player
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    // 🔥 USE TRIGGER INSTEAD OF COLLISION
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Register hit
            FindObjectOfType<ChaseManager1>().PlayerHit();

            // Destroy bullet
            Destroy(gameObject);
        }
    }
}
