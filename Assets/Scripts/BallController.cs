using UnityEngine;
using System.Collections;  // Add this for IEnumerator

public class BallController : MonoBehaviour
{
    [SerializeField] private float force = 1f;
    [SerializeField] private Transform ballAnchor;
    [SerializeField] private InputManager inputManager;

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

        // Add a small physics update delay
        StartCoroutine(ApplyForceNextFrame());
    }

    private IEnumerator ApplyForceNextFrame()
    {
        // Wait for the next physics update
        yield return new WaitForFixedUpdate();

        // Now apply the force
        ballRB.AddForce(transform.forward * force, ForceMode.Impulse);
    }
}