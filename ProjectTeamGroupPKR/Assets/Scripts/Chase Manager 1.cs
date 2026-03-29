using System.Collections;
using UnityEngine;

public class ChaseManager1 : MonoBehaviour
{
    public float distance = 0f;
    public float speed = 12f;
    public float winDistance = 1000f;

    public int hits = 0;
    public int maxHitsBeforeCatch = 3;

    public Transform officer;
    public float moveCloserAmount = 2f;

    private bool canBeCaught = false;
    private bool chaseActive = false;

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
    }

    public void PlayerHit()
    {
        hits++;

        if (officer != null)
        {
            StartCoroutine(MoveOfficerCloser());
        }

        if (hits >= maxHitsBeforeCatch)
        {
            canBeCaught = true;
        }
    }

    IEnumerator MoveOfficerCloser()
    {
        Vector3 start = officer.position;
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

    public void OfficerCatch()
    {
        if (!canBeCaught) return;
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
