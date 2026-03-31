using System.Collections;
using System.Collections.Generic;
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

    [Header("Audio Sources")]
    public AudioSource runAudio;   // 🔁 LOOP ONLY
    public AudioSource sfxAudio;   // 🔊 ONE SHOTS

    [Header("Audio Clips")]
    public AudioClip runSound;
    public AudioClip jumpSound;

    private bool gameEnded = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        if (gameEnded) return;

        Vector2 move = Vector2.zero;

        // 🔹 PRISON MOVEMENT
        if (!inChaseMode)
        {
            move.x = Input.GetAxis("Horizontal");

            // FOOTSTEPS ONLY WHEN MOVING
            if (Mathf.Abs(move.x) > 0.1f && grounded)
                StartRunSound();
            else
                StopRunSound();

            // JUMP
            if (Input.GetButtonDown("Jump") && grounded)
            {
                velocity.y = jumpTakeOffSpeed;
                PlayJumpSound();
            }
        }
        else
        {
            // 🔹 CHASE MODE
            move.x = 0;

            animator.SetFloat("velocityX", 1f);

            // ALWAYS RUN SOUND
            StartRunSound();

            // DOUBLE JUMP
            if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
            {
                velocity.y = jumpTakeOffSpeed;
                jumpCount++;

                PlayJumpSound();
            }
        }

        // SHORT JUMP CUT
        if (Input.GetButtonUp("Jump") && velocity.y > 0)
        {
            velocity.y *= 0.5f;
        }

        if (grounded)
            jumpCount = 0;

        // SPRITE DIRECTION
        if (!inChaseMode)
        {
            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;
        }

        // ANIMATIONS
        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityY", velocity.y / maxSpeed);

        if (inChaseMode)
            animator.SetFloat("velocityX", 1f);
        else
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }

    // 🔊 PLAY JUMP SOUND (ALWAYS WORKS)
    void PlayJumpSound()
    {
        if (sfxAudio != null && jumpSound != null)
        {
            sfxAudio.PlayOneShot(jumpSound);
        }
    }

    // 🔁 RUN SOUND (SAFE LOOP)
    void StartRunSound()
    {
        if (runAudio == null || runSound == null) return;

        if (!runAudio.isPlaying)
        {
            runAudio.clip = runSound;
            runAudio.loop = true;
            runAudio.Play();
        }
    }

    // 🔇 STOP RUN SOUND
    void StopRunSound()
    {
        if (runAudio != null && runAudio.isPlaying)
        {
            runAudio.Stop();
        }
    }

    public void StartChaseMode()
    {
        inChaseMode = true;
    }

    // 🔥 STOP EVERYTHING
    public void StopAllPlayerAudio()
    {
        gameEnded = true;

        StopRunSound();

        if (sfxAudio != null)
            sfxAudio.Stop();
    }
}
