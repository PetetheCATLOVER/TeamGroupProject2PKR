using UnityEngine;

public class ObstacleHit1 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<ChaseManager1>().PlayerHit();
            Destroy(gameObject);
        }
    }
}
