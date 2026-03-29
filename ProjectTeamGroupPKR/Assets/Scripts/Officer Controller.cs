using UnityEngine;

public class OfficerController : MonoBehaviour
{
    public Transform player;
    public float catchDistance = 1.5f;

    private Animator animator;

    public GameObject bulletPrefab;
    public Transform gunPoint;
    public float fireRate = 2f;

    private float fireTimer = 0f;
    private bool chaseStarted = false;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!chaseStarted) return;

        // Run animation
        animator.SetFloat("velocityX", 1f);
        animator.SetBool("grounded", true);

        // Shoot
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0f;
        }

        // Catch check
        if (Vector2.Distance(transform.position, player.position) <= catchDistance)
        {
            FindObjectOfType<ChaseManager1>().OfficerCatch();
        }
    }

    public void StartChase()
    {
        chaseStarted = true;
        gameObject.SetActive(true); // 🔥 ensures visibility
    }

    void Shoot()
    {
        if (bulletPrefab == null || gunPoint == null) return;

        Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);
    }
}
