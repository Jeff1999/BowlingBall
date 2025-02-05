using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
    [SerializeField] private float force = 1f;
    [SerializeField] private Transform ballAnchor;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Transform launchIndicator;  // Add this line

    private bool isBallLaunched;
    private Rigidbody ballRB;

    void Start()
    {
        ballRB = GetComponent<Rigidbody>();
        inputManager.OnSpacePressed.AddListener(LaunchBall);
        transform.parent = ballAnchor;
        transform.localPosition = Vector3.zero;
        ballRB.isKinematic = true;
    }

    private void LaunchBall()
    {
        if (isBallLaunched) return;
        isBallLaunched = true;

        transform.parent = null;
        ballRB.isKinematic = false;

        // Store the launch direction before starting coroutine
        Vector3 launchDirection = launchIndicator.forward;

        // Pass the direction to the coroutine
        StartCoroutine(ApplyForceNextFrame(launchDirection));

        // Hide the indicator
        launchIndicator.gameObject.SetActive(false);
    }

    private IEnumerator ApplyForceNextFrame(Vector3 direction)
    {
        yield return new WaitForFixedUpdate();
        ballRB.AddForce(direction * force, ForceMode.Impulse);
    }
}
