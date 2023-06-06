using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatIndicatorController : MonoBehaviour
{
    public RectTransform leftIndicator; // Reference to the left image's RectTransform
    public RectTransform rightIndicator; // Reference to the right image's RectTransform
    public BeatController beatController; // Reference to the BeatController

    private float maxDistance = 100; // Maximum distance from the center to either end of the bar
    private float currentDistance; // Current distance from the center

    // Start is called before the first frame update
    void Start()
    {
        beatController = GameObject.FindObjectOfType<BeatController>();
        if (beatController == null)
        {
            Debug.LogError("No BeatController found in the scene. Please add one.");
        }

        if (leftIndicator == null || rightIndicator == null)
        {
            Debug.LogError("Both leftIndicator and rightIndicator should be assigned in the Inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the current distance based on the beat timer
        currentDistance = maxDistance * (beatController.GetTimer() / beatController.GetBeatInterval());


        // Set the position of the indicators
        leftIndicator.anchoredPosition = new Vector2(-currentDistance, 0);
        rightIndicator.anchoredPosition = new Vector2(currentDistance, 0);
    }
}
