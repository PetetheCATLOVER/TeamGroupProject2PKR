using UnityEngine;

public class OfficerController : MonoBehaviour
{
    public Transform player;
    public float catchDistance = 1.5f;
    public bool chaseStarted = false;

    [Header("Animation")]
    private Animator animator;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform gunPoint;
    public float fireRate = 2f;

    private float fireTimer = 0f;

    private bool gameEnded = false;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (gameEnded) return;
        if (!chaseStarted) return;

        // 🏃 FORCE RUN ANIMATION
        animator.SetFloat("velocityX", 1f);
        animator.SetBool("grounded", true);

        // 🔫 SHOOT TIMER
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0f;
        }

        // 🚓 CHECK DISTANCE TO PLAYER
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= catchDistance)
        {
            FindObjectOfType<ChaseManager1>().OfficerCatch();
        }
    }

    public void StartChase()
    {
        chaseStarted = true;

        Debug.Log("OFFICER STARTED");
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);
    }

    // 🔥 STOP ALL AUDIO (FIX LOOP BUG)
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
