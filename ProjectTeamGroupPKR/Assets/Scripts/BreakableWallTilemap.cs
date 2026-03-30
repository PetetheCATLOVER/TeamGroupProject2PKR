using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableWallTilemap : MonoBehaviour
{
    private bool playerTouching = false;
    private bool isBroken = false;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip breakSound;

    private TilemapCollider2D tilemapCollider;
    private TilemapRenderer tilemapRenderer;

    void Awake()
    {
        tilemapCollider = GetComponent<TilemapCollider2D>();
        tilemapRenderer = GetComponent<TilemapRenderer>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 🔑 Press E ONLY when touching wall
        if (playerTouching && !isBroken && Input.GetKeyDown(KeyCode.E))
        {
            BreakWall();
        }
    }

    void BreakWall()
    {
        isBroken = true;

        // 🔊 Play sound
        if (audioSource != null && breakSound != null)
        {
            audioSource.PlayOneShot(breakSound);
        }

        // 💥 Disable wall
        tilemapCollider.enabled = false;
        tilemapRenderer.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTouching = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTouching = false;
        }
    }
}
