using UnityEngine;

public class StartChaseTrigger : MonoBehaviour
{
    public PlayerPlatformerController1 player;
    public ChaseManager1 chaseManager;
    public BackgroundScroller[] backgrounds;
    public GameObject officer;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.StartChaseMode();
            chaseManager.StartChase();

            // Enable background movement
            foreach (var bg  in backgrounds)
            {
                bg.enabled = true;
            }

            // Spawn officer
            officer.SetActive(true);
        }
    }
}