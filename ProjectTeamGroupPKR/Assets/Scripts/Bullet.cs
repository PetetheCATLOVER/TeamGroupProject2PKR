using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 5f;

    private bool passedPlayer = false;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // ✅ SCORE ONLY IF DODGED
        if (!passedPlayer && transform.position.x < 0f)
        {
            FindObjectOfType<ChaseManager1>().AddScore(10);
            passedPlayer = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            passedPlayer = true;

            FindObjectOfType<ChaseManager1>().PlayerHit();

            Destroy(gameObject);
        }
    }
}