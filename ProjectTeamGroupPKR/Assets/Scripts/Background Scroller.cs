using UnityEngine;


public class BackgroundScroller : MonoBehaviour
{
    public Transform bg1;
    public Transform bg2;


    public float scrollSpeed = 8f;
    public float backgroundWidth = 20f;


    public int loopsToWin = 15;


    private int loopCount = 0;
    private bool gameEnded = false;


    void Update()
    {
        if (gameEnded) return;


        // 👉 MOVE RIGHT
        bg1.Translate(Vector2.right * scrollSpeed * Time.deltaTime);
        bg2.Translate(Vector2.right * scrollSpeed * Time.deltaTime);


        // When bg1 goes too far right → reset it to the left
        if (bg1.position.x >= backgroundWidth)
        {
            Reposition(bg1, bg2);
        }


        // When bg2 goes too far right → reset it to the left
        if (bg2.position.x >= backgroundWidth)
        {
            Reposition(bg2, bg1);
        }
    }


    void Reposition(Transform current, Transform other)
    {
        current.position = new Vector3(
            other.position.x - backgroundWidth,
            current.position.y,
            current.position.z
        );


        loopCount++;


        Debug.Log("Loops: " + loopCount);


        if (loopCount >= loopsToWin)
        {
            gameEnded = true;
            FindObjectOfType<GameManager>().WinGame();
        }
    }
}
