using System.Collections;
using UnityEngine;
using TMPro;

public class ChaseManager1 : MonoBehaviour
{
    [Header("Distance")]
    public float distance = 0f;
    public float speed = 12f;
    public float winDistance = 1000f;

    [Header("Score")]
    public int score = 0;
    public TMP_Text scoreText;

    [Header("UI")]
    public GameObject winText;
    public GameObject loseText;

    [Header("Hit System")]
    public int hits = 0;
    public int maxHitsBeforeCatch = 3;

    [Header("Officer")]
    public Transform officer;
    public float moveCloserAmount = 2f;

    public bool canBeCaught = false;
    public bool chaseActive = false;

    [Header("References")]
    public PlayerController1 player;
    public OfficerController officerScript;

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

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
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
        chaseActive = false;

        winText.SetActive(true);

        player.StopAllPlayerAudio();
        officerScript.StopAllOfficerAudio();

        Time.timeScale = 0;
    }

    void LoseGame()
    {
        chaseActive = false;

        loseText.SetActive(true);

        player.StopAllPlayerAudio();
        officerScript.StopAllOfficerAudio();

        Time.timeScale = 0;
    }
}
