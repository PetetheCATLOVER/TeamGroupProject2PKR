using UnityEngine;

public class ObstacleHit1 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ChaseManager1 manager = FindObjectOfType<ChaseManager1>();

            if (manager != null)
            {
                manager.PlayerHit();
            }

            Destroy(gameObject);
        }
    }
}

