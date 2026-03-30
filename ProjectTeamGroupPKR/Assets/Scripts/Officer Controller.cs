using UnityEngine;

public class OfficerController : MonoBehaviour
{
    public Transform player;
    public bool chaseStarted = false;

    public float catchDistance = 1.5f;

    private Animator animator;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform gunPoint;
    public float fireRate = 2f;

    private float fireTimer = 0f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip runSound;
    public AudioClip gunSound;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!chaseStarted) return;

        // RUN animation
        animator.SetFloat("velocityX", 1f);
        animator.SetBool("grounded", true);

        PlayRunSound();

        // SHOOT
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0f;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= catchDistance)
        {
            FindObjectOfType<ChaseManager1>().OfficerCatch();
        }
    }

    public void StartChase()
    {
        chaseStarted = true;
        gameObject.SetActive(true);
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);

        audioSource.PlayOneShot(gunSound);
    }

    void PlayRunSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = runSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void StopAllAudio()
    {
        audioSource.Stop();
    }
}
