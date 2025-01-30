using UnityEngine;

public class Gutter : MonoBehaviour
{
    private void OnTriggerEnter(Collider triggeredBody)
    {
        // Get the Rigidbody of the ball
        Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();

        // If the ball has no Rigidbody, exit
        if (ballRigidBody == null) return;

        // Store the ball's speed before stopping it
        float velocityMagnitude = ballRigidBody.linearVelocity.magnitude;

        // Reset the ball's movement
        ballRigidBody.linearVelocity = Vector3.zero;
        ballRigidBody.angularVelocity = Vector3.zero;

        // Move the ball forward in the gutter's direction
        ballRigidBody.AddForce(transform.forward * velocityMagnitude, ForceMode.VelocityChange);
    }
}

