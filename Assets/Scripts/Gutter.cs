using UnityEngine;

public class Gutter : MonoBehaviour
{
    private void OnTriggerEnter(Collider triggeredBody)
    {
        // Get the rigidbody of the ball
        Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();

        // Store the current velocity magnitude
        float velocityMagnitude = ballRigidBody.linearVelocity.magnitude;

        // Reset both linear and angular velocity
        ballRigidBody.linearVelocity = Vector3.zero;
        ballRigidBody.angularVelocity = Vector3.zero;

        // Add force in the gutter's forward direction
        ballRigidBody.AddForce(transform.forward * velocityMagnitude,
            ForceMode.VelocityChange);
    }
}

