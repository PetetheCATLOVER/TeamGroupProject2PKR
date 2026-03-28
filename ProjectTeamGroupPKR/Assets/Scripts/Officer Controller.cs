using UnityEngine;


public class OfficerController : MonoBehaviour
{
    public bool chaseStarted = false;


    private Animator animator;


    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform gunPoint;
    public float fireRate = 2f;


    private float fireTimer = 0f;


    void Awake()
    {
        // 🔥 FIX: Get correct animator (NOT gun)
        animator = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        if (!chaseStarted) return;


        // ✅ FORCE RUN ANIMATION (same system as player)
        animator.SetFloat("velocityX", 1f);


        // 🔫 SHOOTING
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


        // 🔥 Start running immediately
        animator.SetFloat("velocityX", 1f);
    }


    void Shoot()
    {
        Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);
    }
}
