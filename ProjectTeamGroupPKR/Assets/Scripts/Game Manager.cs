using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public OfficerController officer;
    public CameraTransition cameraTransition;

    public void StartChaseSequence()
    {
        player.inChaseMode = true;
        officer.StartChase();
        cameraTransition.StartChase();
    }

    public void LoseGame()
    {
        Debug.Log("Player caught! Game Over.");
        Time.timeScale = 0;
    }

    public void WinGame()
    {
        Debug.Log("Escape successful!");
        Time.timeScale = 0;
    }

}
