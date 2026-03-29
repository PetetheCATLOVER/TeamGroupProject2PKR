using UnityEngine;

public class ObstacleSpawner1 : MonoBehaviour
{
    public GameObject[] obstacles;
    public float spawnTime = 2f;
    public Transform spawnPoint;

    public bool canSpawn = false;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 2f, spawnTime);
    }

    void SpawnObstacle()
    {
        if (!canSpawn || spawnPoint == null || obstacles.Length == 0) return;

        int rand = Random.Range(0, obstacles.Length);

        GameObject obj = Instantiate(obstacles[rand], spawnPoint.position, Quaternion.identity);

        if (obj.GetComponent<ObstacleMover>() == null)
        {
            obj.AddComponent<ObstacleMover>();
        }
    }

    public void StartSpawning()
    {
        canSpawn = true;
    }
}
