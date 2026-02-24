using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public Transform thirdPersonView;
    public Transform sideView2D;
    public float transitionSpeed = 2f;

    private bool switchTo2D = false;

    void Update()
    {
        if (switchTo2D)
        {
            transform.position = Vector3.Lerp(transform.position, sideView2D.position, Time.deltaTime * transitionSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, sideView2D.rotation, Time.deltaTime * transitionSpeed);
        }

    }
    public void StartChase()
    {
        switchTo2D = true;
    }
}