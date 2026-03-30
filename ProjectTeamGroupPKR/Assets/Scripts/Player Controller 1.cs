using System.Collections;
using UnityEngine;

public class PlayerController1 : PhysicsObject
{
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    [Header("Chase Settings")]
    public bool inChaseMode = false;
    public int maxJumps = 2;

    private int jumpCount = 0;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip runSound;
    public AudioClip jumpSound;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        if (!inChaseMode)
        {
            move.x = Input.GetAxis("Horizontal");

            // ✅ FIX CHARACTER DIRECTION
            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            // ✅ PLAY RUN SOUND WHEN MOVING IN CELL
            if (Mathf.Abs(move.x) > 0.1f && grounded)
            {
                PlayRunSound();
            }
            else
            {
                StopRunSound();
            }

            // ✅ SINGLE JUMP
            if (Input.GetButtonDown("Jump") && grounded)
            {
                StartCoroutine(Jump());
                PlayJumpSound();
            }
        }
        else
        {
            move.x = 0;

            // ✅ FORCE RUN ANIMATION IN CHASE
            animator.SetFloat("velocityX", 1f);

            // ✅ ALWAYS PLAY RUN SOUND IN CHASE
            PlayRunSound();

            // ✅ DOUBLE JUMP
            if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
            {
                velocity.y = jumpTakeOffSpeed;
                jumpCount++;

                animator.SetFloat("velocityY", velocity.y);
                PlayJumpSound();
            }
        }

        // ✅ SHORT JUMP CUT
        if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
                velocity.y *= 0.5f;
        }

        // ✅ RESET JUMPS
        if (grounded)
            jumpCount = 0;

        // ✅ ANIMATIONS
        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityY", velocity.y / maxSpeed);

        if (!inChaseMode)
            animator.SetFloat("velocityX", Mathf.Abs(move.x));
        else
            animator.SetFloat("velocityX", 1f);

        targetVelocity = move * maxSpeed;
    }

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.1f);
        velocity.y = jumpTakeOffSpeed;
    }

    public void StartChaseMode()
    {
        inChaseMode = true;
    }

    // 🎧 AUDIO FUNCTIONS

    void PlayRunSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = runSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void StopRunSound()
    {
        if (audioSource.isPlaying && audioSource.clip == runSound)
        {
            audioSource.Stop();
        }
    }

    void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void StopAllAudio()
    {
        audioSource.Stop();
    }
}
