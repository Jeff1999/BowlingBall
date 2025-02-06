using System;
using UnityEngine;
using UnityEngine.Events;

public class FallTrigger : MonoBehaviour
{
    // Event that other objects can subscribe to
    public UnityEvent OnPinFall = new();
    public bool isPinFallen = false;

    private void OnTriggerEnter(Collider triggeredObject)
    {
        // Only trigger if it's the ground AND the pin hasn't already fallen
        if (triggeredObject.CompareTag("Ground") && !isPinFallen)
        {
            isPinFallen = true;
            OnPinFall?.Invoke();
            Debug.Log($"{gameObject.name} is fallen");
        }
    }
}
