using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    public float jumpForce = 8f;
    public int maxJumps = 2;

    private int jumpCount = 0;
    private Rigidbody2D rb;

    public Animator animator;
    public AudioSource audioSource;
    public AudioClip jumpSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        jumpCount++;

        animator.SetTrigger("Jump");

        audioSource.PlayOneShot(jumpSound);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }
}