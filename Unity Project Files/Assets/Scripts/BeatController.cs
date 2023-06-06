using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatController : MonoBehaviour
{
    public float beatsPerMinute = 120f; // BPM value
    public bool isBeatOn = false; // Indicates if the beat is currently "on"
    public AudioClip beatSound; // Sound bite to play when the beat is on
    public float gracePeriod = 0.1f; // Grace period duration in seconds

    private float beatInterval; // Time interval between beats
    private float timer; // Timer to keep track of the beat timing
    private bool hasActedOnBeat = false; // Indicates if the player has acted on the current beat

    private AudioSource audioSource; // Reference to the AudioSource component

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the beat interval based on the BPM value
        beatInterval = 60f / beatsPerMinute;
        timer = 0f;

        // Get the AudioSource component attached to this object
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // If no AudioSource component is found, add one to the object
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update the timer with the time elapsed since the last frame
        timer += Time.deltaTime;

        // Check if it's time for the next beat
        if (timer >= beatInterval)
        {
            // Reset the timer
            timer -= beatInterval;

            // Toggle the beat state
            isBeatOn = !isBeatOn;
            hasActedOnBeat = false;

            // Play sound or trigger other actions when the beat is "on"
            if (isBeatOn)
            {
                // Play sound if a sound bite is attached
                if (beatSound != null)
                {
                    audioSource.PlayOneShot(beatSound);
                }
            }
        }
    }

    // Check if the player has acted on the beat this beat
    public bool HasActedOnBeat()
    {
        return hasActedOnBeat;
    }

    // Called by other scripts to indicate that the player has acted on the beat this beat
    public void PlayerActedOnBeat()
    {
        hasActedOnBeat = true;
    }

    // Check if the player is within the grace period before or after the beat
    public bool IsWithinGracePeriod()
    {
        // Check if the current time is within the grace period
        float graceStartTime = beatInterval - gracePeriod;
        float graceEndTime = beatInterval + gracePeriod;

        return timer >= graceStartTime && timer <= graceEndTime;
    }
}
