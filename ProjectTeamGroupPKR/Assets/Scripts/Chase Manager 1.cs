using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ChaseManager1 : MonoBehaviour
{
    [Header("Distance")]
    public float distance = 0f;
    public float speed = 12f;
    public float winDistance = 1000f;

    [Header("UI")]
    public TMP_Text distanceText;
    public GameObject winText;
    public GameObject loseText;
    public GameObject pauseMenu;

    [Header("Hit System")]
    public int hits = 0;
    public int maxHitsBeforeCatch = 3;

    [Header("Officer")]
    public Transform officer;
    public float moveCloserAmount = 2f;

    public bool canBeCaught = false;
    public bool chaseActive = false;
    private bool isPaused = false;

    [Header("References")]
    public PlayerController1 player;
    public OfficerController officerScript;

    void Update()
    {
        if (!chaseActive || isPaused) return;

        distance += speed * Time.deltaTime;

        if (distanceText != null)
        {
            distanceText.text = "Distance: " + Mathf.FloorToInt(distance) + "m";
        }

        if (distance >= winDistance)
        {
            WinGame();
        }
    }

    // 🚨 START CHASE
    public void StartChase()
    {
        chaseActive = true;
        distance = 0f;
        hits = 0;
        canBeCaught = false;
    }

    // 💥 HIT SYSTEM
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

        float t = 0;

        while (t < 0.3f)
        {
            officer.position = Vector3.Lerp(start, target, t / 0.3f);
            t += Time.deltaTime;
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
        EndGame(winText);
    }

    void LoseGame()
    {
        EndGame(loseText);
    }

    void EndGame(GameObject screen)
    {
        chaseActive = false;

        if (screen != null)
            screen.SetActive(true);

        if (player != null)
            player.StopAllPlayerAudio();

        if (officerScript != null)
            officerScript.StopAllOfficerAudio();

        Time.timeScale = 0;
    }

    // 🔄 RESTART
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ❌ QUIT (back to start scene)
    public void QuitToStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ⏸️ PAUSE
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;

        if (pauseMenu != null)
            pauseMenu.SetActive(true);
    }

    // ▶️ RESUME
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;

        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }
}
