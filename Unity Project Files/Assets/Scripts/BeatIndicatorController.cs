using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatIndicatorController : MonoBehaviour
{
    public RectTransform leftIndicator; // Reference to the left image's RectTransform
    public RectTransform rightIndicator; // Reference to the right image's RectTransform
    public BeatController beatController; // Reference to the BeatController

    private Vector2 leftStartPos; // Starting position of the left indicator
    private Vector2 rightStartPos; // Starting position of the right indicator

    private float timer; // Timer to keep track of the time elapsed since the last reset

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
        else
        {
            // Store the starting positions of the indicators
            leftStartPos = leftIndicator.anchoredPosition;
            rightStartPos = rightIndicator.anchoredPosition;
        }
    }

    void Update()
    {
        if (beatController.isBeatOn)
        {
            // Reset the position of the indicators and timer
            leftIndicator.anchoredPosition = leftStartPos;
            rightIndicator.anchoredPosition = rightStartPos;
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;

            // Move the indicators towards the center on the x-axis only
            float leftNewX = Mathf.Lerp(leftStartPos.x, 0, timer / beatController.GetBeatInterval());
            float rightNewX = Mathf.Lerp(rightStartPos.x, 0, timer / beatController.GetBeatInterval());

            leftIndicator.anchoredPosition = new Vector2(leftNewX, leftIndicator.anchoredPosition.y);
            rightIndicator.anchoredPosition = new Vector2(rightNewX, rightIndicator.anchoredPosition.y);
        }
    }
}
