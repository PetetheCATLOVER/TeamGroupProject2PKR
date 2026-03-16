using UnityEngine;

public class ObstacleSpawner1 : MonoBehaviour
{
    
    public GameObject[] obstacles;

    public float spawnTime = 2f;
    public Transform spawnPoint;

    void Start()
    {
        InvokeRepeating("SpawnObstacle", 2f, spawnTime);
    }

    void SpawnObstacle()
    {
        int rand = Random.Range(0, obstacles.Length);

        Instantiate(obstacles[rand], spawnPoint.position, Quaternion.identity);
    }
}