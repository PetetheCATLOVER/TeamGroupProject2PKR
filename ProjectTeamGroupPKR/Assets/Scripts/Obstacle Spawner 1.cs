using UnityEngine;

public class ObstacleSpawner1 : MonoBehaviour
{
    public GameObject[] obstacles;
    public float spawnTime = 2f;
    public Transform spawnPoint;

    private bool spawning = false;

    public void StartSpawning()
    {
        if (spawning) return;

        spawning = true;
        InvokeRepeating("SpawnObstacle", 1f, spawnTime);
    }

    void SpawnObstacle()
    {
        int rand = Random.Range(0, obstacles.Length);

        GameObject obj = Instantiate(obstacles[rand], spawnPoint.position, Quaternion.identity);

        // 🔥 GIVE IT MOVEMENT
    }
}
