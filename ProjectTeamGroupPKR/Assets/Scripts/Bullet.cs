using UnityEngine;


public class Bullet : MonoBehaviour
{
    public float speed = 10f;


    void Update()
    {
        // Move LEFT toward player
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<ChaseManager1>().PlayerHit();
        }


        Destroy(gameObject);
    }
}
