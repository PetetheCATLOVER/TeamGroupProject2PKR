using UnityEngine;

public class ObstacleHit1 : MonoBehaviour
{
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<ChaseManager1>().PlayerHit();

            Destroy(gameObject);
        }
    }

}
