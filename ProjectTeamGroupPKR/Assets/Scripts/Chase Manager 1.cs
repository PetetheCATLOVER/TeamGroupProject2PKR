using System.Collections;
using UnityEngine;

public class ChaseManager1 : MonoBehaviour
{
    [Header("Distance Settings")]
    public float distance = 0f;
    public float speed = 12f;
    public float winDistance = 1000f;

    [Header("Hit System")]
    public int hits = 0;
    public int maxHitsBeforeCatch = 3; // NOT game over anymore

    [Header("Officer System")]
    public Transform officer;
    public float moveCloserAmount = 2f;

    public bool canBeCaught = false;

    [Header("Game State")]
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

        Debug.Log("Player Hit! Total Hits: " + hits);

        // 🚓 Move officer closer smoothly
        if (officer != null)
        {
            StartCoroutine(MoveOfficerCloser());
        }

        // 🔥 AFTER 3 HITS → PLAYER CAN BE CAUGHT
        if (hits >= maxHitsBeforeCatch)
        {
            canBeCaught = true;
            Debug.Log("Officer is now close enough to catch!");
        }
    }

    IEnumerator MoveOfficerCloser()
    {
        Vector3 start = officer.position;

        // 👉 YOUR DIRECTION (OFFICER ON RIGHT → MOVE LEFT)
        Vector3 target = start - new Vector3(moveCloserAmount, 0, 0);

        float time = 0;

        while (time < 0.3f)
        {
            officer.position = Vector3.Lerp(start, target, time / 0.3f);
            time += Time.deltaTime;
            yield return null;
        }

        officer.position = target;
    }

    // 🔥 CALLED BY OFFICER WHEN CLOSE
    public void OfficerCatch()
    {
        if (!canBeCaught) return;

        Debug.Log("OFFICER CAUGHT YOU!");
        LoseGame();
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
