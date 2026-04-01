using UnityEngine;

public class StartChaseTrigger : MonoBehaviour
{
    public ObstacleSpawner1 spawner;
    public PlayerController1 player;
    public OfficerController officer;
    public ChaseManager1 chaseManager;
    public BackgroundScroller backgroundScroller;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.StartChaseMode();

            // 🚶 OFFICER WALKS IN INSTEAD OF SPAWNING
            officer.StartEntrance();

            // 🎮 START GAME SYSTEMS (BUT OFFICER SHOOTS LATER)
            chaseManager.StartChase();
            backgroundScroller.StartScrolling();

            if (spawner != null)
                spawner.StartSpawning();
        }
    }
}

