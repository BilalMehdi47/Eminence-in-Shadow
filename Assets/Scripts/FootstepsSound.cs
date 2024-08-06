using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class FootstepsSound : MonoBehaviour
// {
//    private AudioSource audioSource;

// [Header("FootSteps Sources")]
// public AudioClip[] footstepsSound;

// private void Awake()
// {
//     audioSource = GetComponent<AudioSource>();
// }

// private AudioClip GetRandomFootStep()
// {
//     return footstepsSound[UnityEngine.Random.Range(0, footstepsSound.Length)];
// }

// private void Step()
// {
//     AudioClip clip = GetRandomFootStep();
//     audioSource.PlayOneShot(clip);
// }

// }



public class FootstepsSound : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("FootSteps Sources")]
    public AudioClip[] footstepsSound;

    private float stepCooldown = 0.2f;  // Minimum time between each footstep sound
    private float lastStepTime = -0.2f; // Initialize to allow playing immediately

    // Assuming you have a way to check if the player is moving. For example:
    public bool isMoving = false; // This should be set based on actual movement input

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Update isMoving based on your input system
        // Example for keyboard (you should replace this with your actual movement input check)
        CheckMovement();

        // Only try to play footstep if the player is moving
        if (isMoving)
        {
            TryPlayFootstep();
        }
    }

    private void CheckMovement()
    {
        // Example check for movement (you need to adapt this to your input system)
        isMoving = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D));
    }

    private void TryPlayFootstep()
    {
        if (Time.time - lastStepTime >= stepCooldown)
        {
            Step();
            lastStepTime = Time.time;
        }
    }

    private AudioClip GetRandomFootStep()
    {
        return footstepsSound[UnityEngine.Random.Range(0, footstepsSound.Length)];
    }

    private void Step()
    {
        AudioClip clip = GetRandomFootStep();
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}








