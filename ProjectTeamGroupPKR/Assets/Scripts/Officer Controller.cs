using UnityEngine;


public class OfficerController : MonoBehaviour
{
    public Transform player;
    public float chaseSpeed = 6f;
    public bool chaseStarted = false;


    [Header("Animation")]
    private Animator animator;


    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform gunPoint;
    public float fireRate = 2f;


    private float fireTimer = 0f;


    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        if (!chaseStarted) return;

        // ❌ REMOVE movement completely

        // ✅ FORCE RUN ANIMATION
        animator.SetFloat("velocityX", 1f);
        animator.SetBool("grounded", true);

        // 🔫 SHOOT TIMER
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0f;
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
}
