using System.Collections;
using UnityEngine;

public class OfficerController : MonoBehaviour
{
    public Transform player;
    public float catchDistance = 1.5f;

    [Header("Entrance Movement")]
    public Vector3 startPosition = new Vector3(4f, -2f, 0f);
    public Vector3 targetPosition = new Vector3(1f, -2f, 0f);
    public float walkInSpeed = 3f;

    private bool isWalkingIn = false;
    private bool chaseStarted = false;
    private bool gameEnded = false;

    [Header("Animation")]
    private Animator animator;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform gunPoint;
    public float fireRate = 2f;
    private float fireTimer;

    [Header("Audio")]
    public AudioSource sirenAudio;     // 🚨 looping siren
    public AudioSource gunshotAudio;   // 🔫 single shot sound

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (gameEnded) return;

        if (isWalkingIn)
        {
            WalkIntoScene();
            return;
        }

        if (!chaseStarted) return;

        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0f;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= catchDistance)
        {
            FindObjectOfType<ChaseManager1>().OfficerCatch();
        }
    }

    void WalkIntoScene()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            walkInSpeed * Time.deltaTime
        );

        animator.SetFloat("velocityX", 1f);
        animator.SetBool("grounded", true);

        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            isWalkingIn = false;
            StartChase();
        }
    }

    public void StartEntrance()
    {
        transform.position = startPosition;
        gameObject.SetActive(true);

        isWalkingIn = true;
        chaseStarted = false;

        // 🚨 START SIREN WHEN OFFICER APPEARS
        if (sirenAudio != null && !sirenAudio.isPlaying)
        {
            sirenAudio.Play();
        }
    }

    void StartChase()
    {
        chaseStarted = true;
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);

        // 🔫 PLAY GUNSHOT SOUND EVERY SHOT
        if (gunshotAudio != null)
        {
            gunshotAudio.Play();
        }
    }

    public void StopAllOfficerAudio()
    {
        gameEnded = true;

        if (sirenAudio != null)
            sirenAudio.Stop();

        if (gunshotAudio != null)
            gunshotAudio.Stop();
    }
}
