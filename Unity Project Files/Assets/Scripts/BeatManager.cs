using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public float bpm; // Beats per minute
    private float beatInterval, beatTimer, beatCounter;

    // Use this for initialization
    void Start()
    {
        // Calculate how many seconds is one beat
        // We divide by 60 because there are 60 seconds in a minute
        beatInterval = 60/bpm;
        beatTimer = beatInterval;
        beatCounter = 0;
    }

    void Update()
    {
        // If the time exceeded the beat interval
        if (beatTimer > beatInterval)
        {
            beatTimer -= beatInterval; // Reset timer
            beatCounter++; // Increase beat count
        }

        beatTimer += Time.deltaTime; // Increase timer
    }

    public bool IsBeat()
    {
        // A method for other scripts to check if it's time to act
        return (beatCounter > 0);
    }

    public void ActedOnBeat()
    {
        // A method for other scripts to say that they acted on the beat
        beatCounter--;
    }
}
