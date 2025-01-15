using UnityEngine;
using System.Collections;
using TMPro;
//using Oculus.Interaction.Samples;

public class CountdownStarter : MonoBehaviour
{
   //private CountdownTimer _countdownTimer;
    [SerializeField] private TextMeshPro _timerText; // Reference to the TextMeshPro object
    [SerializeField] private float countdownTime = 600; // Countdown duration in seconds (10 minutes)

    private float currentTime; // Current time remaining
    private bool countdownActive = false; // Controls whether the countdown is running


    private void Start()
    {

        // Set the timer to start after 5 seconds
        StartCoroutine(StartCountdownAfterDelay(5));

        // // Get the CountdownTimer component
        // _countdownTimer = GetComponent<CountdownTimer>();
                // // Ensure the CountdownTimer component exists
        // if (_countdownTimer == null)
        // {
        //     Debug.LogError("CountdownTimer component is missing from the GameObject.");
        //     return;
        // }

        // Start the countdown after 5 seconds
        //StartCoroutine(StartTimerAfterDelay(5));
    }

    // private IEnumerator StartTimerAfterDelay(float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     _countdownTimer.CountdownOn = true; // Enable the timer
    // }

   private IEnumerator StartCountdownAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentTime = countdownTime; // Initialize the timer
        countdownActive = true; // Activate the countdown
    }

    private void Update()
    {
        // Run the countdown if active
        if (countdownActive && currentTime > 0)
        {
            currentTime -= Time.deltaTime; // Decrease time
            UpdateTimerDisplay(currentTime); // Update the display

            // When the timer reaches 0, stop the countdown
            if (currentTime <= 0)
            {
                currentTime = 0;
                countdownActive = false;
                OnTimerEnd(); // Optional: Add end-of-timer behavior
            }
        }
    }

    private void UpdateTimerDisplay(float time)
    {
        // Convert time to minutes and seconds
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        _timerText.text = $"{minutes:00}:{seconds:00}"; // Update the TextMeshPro text
    }

    private void OnTimerEnd()
    {
        Debug.Log("Timer Ended!"); // Replace with your desired action
    }
}



