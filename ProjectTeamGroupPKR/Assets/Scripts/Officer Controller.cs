using System.Collections;
using UnityEngine;

public class OfficerController : MonoBehaviour
{
    public Transform player;
    public float catchDistance = 1.5f;

    [Header("Entrance Movement")]
    public Vector3 startPosition = new Vector3(4f, -2f, 0f); // ✅ FIXED
    public Vector3 targetPosition = new Vector3(1f, -2f, 0f); // ✅ FIXED
    public float walkInSpeed = 3f;

    private bool isWalkingIn = false;
    private bool chaseStarted = false;

    [Header("Animation")]
    private Animator animator;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform gunPoint;
    public float fireRate = 2f;

    private float fireTimer = 0f;

    [Header("Audio")]
    public AudioSource gunAudio;     // 🔊 NEW
    public AudioClip gunShotSound;   // 🔊 NEW

    private bool gameEnded = false;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (gameEnded) return;

        // 🚶 WALK INTO SCENE FIRST
        if (isWalkingIn)
        {
            WalkIntoScene();
            return;
        }

        if (!chaseStarted) return;

        // 🏃 RUN ANIMATION
        animator.SetFloat("velocityX", 1f);
        animator.SetBool("grounded", true);

        // 🔫 SHOOT TIMER
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0f;
        }

        // 🚓 CHECK DISTANCE
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= catchDistance)
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
            StartActualChase();
        }
    }

    public void StartEntrance()
    {
        transform.position = startPosition;
        gameObject.SetActive(true);

        isWalkingIn = true;
        chaseStarted = false;
    }

    void StartActualChase()
    {
        chaseStarted = true;
        Debug.Log("OFFICER CHASE STARTED AFTER WALK-IN");
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);

        // 🔊 PLAY GUN SOUND (FIX)
        if (gunAudio != null && gunShotSound != null)
        {
            gunAudio.PlayOneShot(gunShotSound);
        }
    }

    public void StopAllOfficerAudio()
    {
        gameEnded = true;

        AudioSource[] audios = GetComponentsInChildren<AudioSource>();

        foreach (AudioSource a in audios)
        {
            a.Stop();
        }
    }
}

