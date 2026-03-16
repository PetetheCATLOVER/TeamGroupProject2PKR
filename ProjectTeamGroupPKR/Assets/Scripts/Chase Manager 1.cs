using UnityEngine;

public class ChaseManager1 : MonoBehaviour
{
    
    public float distance = 0f;
    public float speed = 12f;
    public float winDistance = 1000f;

    public int hits = 0;
    public int maxHits = 3;

    public bool chaseActive = false;

    void Update()
    {
        if (!chaseActive) return;

        distance += speed * Time.deltaTime;

        if (distance >= winDistance)
        {
            WinGame();
        }
    }

    public void StartChase()
    {
        chaseActive = true;
        Debug.Log("CHASE STARTED");
    }

    public void PlayerHit()
    {
        hits++;

        if (hits >= maxHits)
        {
            LoseGame();
        }
    }

    void WinGame()
    {
        Debug.Log("ESCAPED! YOU WIN!");
        Time.timeScale = 0;
    }

    void LoseGame()
    {
        Debug.Log("CAUGHT! GAME OVER!");
        Time.timeScale = 0;
    }
}