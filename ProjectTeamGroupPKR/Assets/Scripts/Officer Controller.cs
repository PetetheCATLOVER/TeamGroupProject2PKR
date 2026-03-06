using UnityEngine;

public class OfficerController : MonoBehaviour
{
    public Transform player;
    public float chaseSpeed = 6f;
    public bool chaseStarted = false;

    void Update()
    {
        if (!chaseStarted) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(player.position.x, transform.position.y),
            chaseSpeed * Time.deltaTime
        );

    }
    public void StartChase()
    {
        chaseStarted = true;
    }

}
