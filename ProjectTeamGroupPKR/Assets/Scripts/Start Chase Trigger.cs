using UnityEngine;

public class StartChaseTrigger : MonoBehaviour
{
    public PlayerController1 player;
    public OfficerController officer;
    public ChaseManager1 chaseManager;
    public BackgroundScroller backgroundScroller;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.StartChaseMode();

            officer.gameObject.SetActive(true);

            officer.StartChase();
            chaseManager.StartChase();
            backgroundScroller.StartScrolling(); // 🔥 THIS FIXES BACKGROUND

            Debug.Log("CHASE STARTED");
        }
    }
}
