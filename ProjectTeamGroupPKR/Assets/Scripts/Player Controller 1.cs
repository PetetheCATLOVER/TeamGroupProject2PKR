using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController1 : PhysicsObject
{
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    [Header("Chase Settings")]
    public bool inChaseMode = false;
    public int maxJumps = 2;

    private int jumpCount = 0;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        // 🔹 NORMAL MOVEMENT (INSIDE CELL)
        if (!inChaseMode)
        {
            move.x = Input.GetAxis("Horizontal");

            // SINGLE JUMP ONLY
            if (Input.GetButtonDown("Jump") && grounded)
            {
                StartCoroutine(Jump());
            }
        }
        else
        {
            // 🔹 CHASE MODE (NO LEFT/RIGHT MOVEMENT)
            move.x = 0;

            // DOUBLE JUMP ENABLED
            if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
            {
                velocity.y = jumpTakeOffSpeed;
                jumpCount++;

                animator.SetTrigger("Jump");
            }

            // Always running animation
            animator.SetBool("isRunning", true);
        }

        // SHORT JUMP CUT (same as before)
        if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y *= 0.5f;
            }
        }

        // RESET JUMPS WHEN GROUNDED
        if (grounded)
        {
            jumpCount = 0;
        }

        // 🔹 SPRITE DIRECTION (ONLY IN NORMAL MODE)
        if (!inChaseMode)
        {
            if (move.x > 0.01f)
            {
                spriteRenderer.flipX = false;
            }
            else if (move.x < -0.01f)
            {
                spriteRenderer.flipX = true;
            }
        }

        // 🔹 ANIMATIONS
        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
        animator.SetFloat("velocityY", velocity.y / maxSpeed);

        // 🔹 APPLY MOVEMENT
        targetVelocity = move * maxSpeed;
    }

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.1f);
        velocity.y = jumpTakeOffSpeed;
    }

    // 🔥 CALL THIS WHEN PLAYER ESCAPES TUNNEL
    public void StartChaseMode()
    {
        inChaseMode = true;
    }
}

