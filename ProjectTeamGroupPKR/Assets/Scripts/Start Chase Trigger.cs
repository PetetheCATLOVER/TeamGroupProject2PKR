using UnityEngine;

public class StartChaseTrigger : MonoBehaviour
{
    public ChaseManager1 chaseManager;
    public GameObject officer;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            chaseManager.StartChase();

            officer.SetActive(true);
        }
    }
}