using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float speed = 12f;

    private bool passedPlayer = false;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // ✅ SCORE ONLY IF PASSED PLAYER SAFELY
        if (!passedPlayer && transform.position.x < 0f)
        {
            FindObjectOfType<ChaseManager1>().AddScore(5);
            passedPlayer = true;
        }

        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            passedPlayer = true; // ❌ prevents scoring
        }
    }
}
