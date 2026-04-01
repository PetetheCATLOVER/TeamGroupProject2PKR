using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float speed = 12f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // destroy when off-screen
        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<ChaseManager1>().PlayerHit();
        }
    }
}