using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float score = 0;
    //A reference to our ballController
    [SerializeField] private BallController ball;
    //A reference for our PinCollection prefab we made in Section 2.2
    [SerializeField] private GameObject pinCollection;
    //A reference for an empty GameObject which we'll
    //use to spawn our pin collection prefab
    [SerializeField] private Transform pinAnchor;
    //A reference for our input manager
    [SerializeField] private InputManager inputManager;
    [SerializeField] private TextMeshProUGUI scoreText;
    private FallTrigger[] fallTriggers;
    private GameObject pinObjects;

    private void Start()
    {
        inputManager.OnResetPressed.AddListener(HandleReset);
        SetPins();
    }

    private void HandleReset()
    {
        ball.ResetBall();
        SetPins();
    }

    private void SetPins()
    {
        Debug.Log("SetPins called");

        // Remove old event listeners
        if (fallTriggers != null)
        {
            foreach (FallTrigger trigger in fallTriggers)
            {
                if (trigger != null)
                {
                    trigger.OnPinFall.RemoveListener(IncrementScore);
                }
            }
        }

        // Clean up old pins
        if (pinObjects)
        {
            Debug.Log("Cleaning up old pins");
            foreach (Transform child in pinObjects.transform)
            {
                Destroy(child.gameObject);
            }
            Destroy(pinObjects);
        }


        // Create new pins
        pinObjects = Instantiate(pinCollection, Vector3.zero, Quaternion.identity);
        if (pinObjects != null)
        {
            pinObjects.transform.SetParent(pinAnchor, false);
            pinObjects.transform.position = pinAnchor.position;
            pinObjects.transform.rotation = pinAnchor.rotation;
        }

        // Setup new fall triggers
        fallTriggers = FindObjectsByType<FallTrigger>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        Debug.Log($"Found {fallTriggers.Length} fall triggers");

        foreach (FallTrigger pin in fallTriggers)
        {
            if (pin != null)
            {
                pin.OnPinFall.AddListener(IncrementScore);
            }
        }
    }

    private void IncrementScore()
    {
        score++;
        scoreText.text = $"Score: {score}";
    }
}

