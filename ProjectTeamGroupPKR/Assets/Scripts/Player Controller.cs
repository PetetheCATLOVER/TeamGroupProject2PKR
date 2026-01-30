using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 6f;

    [Header("State")]
    public bool inChaseMode = false;
    public bool isGrounded = true;
    public bool isDead = false;

    [Header("Componenets")]
    public Rigidbody rb;
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip runSound;
    public AudioClip jumpSound;
    public AudioClip hitSound;

    private int hitCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)return;

        if (!inChaseMode) FreeMovement3D();
        else ChaseMovement2D();

        HandleJump();
        HandleCrouch();
    }
    

    //Prison free movement (3D)
    void FreeMovement3D()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v) * moveSpeed;
        rb.angularVelocity = new Vector3(move.x, rb.angularVelocity.y, move.z);

        animator.SetFloat("Speed", rb.linearVelocity.magnitude);
    }

    //Escape chase (2D runner)
    void ChaseMovement2D()
    {
        rb.angularVelocity = new Vector3(runSpeed, rb.linearVelocity.y, 0);

        animator.SetBool("isRunning", true);

        if (!audioSource.isPlaying)
        {
            audioSource.clip = runSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void HandleJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            audioSource.PlayOneShot(jumpSound);
            animator.SetTrigger("Jump");
            isGrounded = false;
        }
    }

    void HandleCrouch()
    {
        animator.SetBool("isCrouching", Input.GetKey(KeyCode.LeftControl));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            hitCount++;
            audioSource.PlayOneShot(hitSound);
            runSpeed -= 1.5f;

            if (hitCount >= 3)
                Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        audioSource.Stop();
        FindObjectOfType<GameManager>().LoseGame();
    }

}
