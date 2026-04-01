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

    [Header("References")]
    public PlayerController1 player;
    public OfficerController officerScript;

    private bool isPaused = false;

    void Update()
    {
        // 🚫 Stop everything if not active or paused
        if (!chaseActive || isPaused) return;

        // 📏 Increase distance
        distance += speed * Time.deltaTime;

        // 🖥️ Update UI
        if (distanceText != null)
        {
            distanceText.text = "Distance: " + Mathf.FloorToInt(distance) + "m";
        }

        // 🏁 Win condition
        if (distance >= winDistance)
        {
            WinGame();
        }
    }

    // ▶️ START CHASE
    public void StartChase()
    {
        chaseActive = true;
        distance = 0f;
        hits = 0;
        canBeCaught = false;
    }

    // 💥 PLAYER HIT
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

    // 🚓 OFFICER MOVES CLOSER
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

    // 🚓 OFFICER CATCH
    public void OfficerCatch()
    {
        if (!canBeCaught) return;

        LoseGame();
    }

    // 🏁 WIN
    void WinGame()
    {
        EndGame(winText);
    }

    // ❌ LOSE
    void LoseGame()
    {
        EndGame(loseText);
    }

    // 🛑 END GAME (USED BY BOTH)
    void EndGame(GameObject screen)
    {
        chaseActive = false;

        if (screen != null)
            screen.SetActive(true);

        // 🔇 Stop audio
        if (player != null)
            player.StopAllPlayerAudio();

        if (officerScript != null)
            officerScript.StopAllOfficerAudio();

        // ⏸️ Freeze game
        Time.timeScale = 0;
    }

    // 🔄 RESTART GAME
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ❌ QUIT TO START (same scene reload)
    public void QuitToStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ⏸️ PAUSE GAME
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;

        if (pauseMenu != null)
            pauseMenu.SetActive(true);
    }

    // ▶️ RESUME GAME
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;

        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }
}
