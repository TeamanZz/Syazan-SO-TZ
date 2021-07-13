using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentinelAnimations : MonoBehaviour
{
    private Animator animator;
    private Sentinel sentinel;

    private const string StopRunningAnimation = "Stop Running";
    private const string velocity = "velocity";

    private void Awake()
    {
        sentinel = GetComponent<Sentinel>();
        animator = GetComponent<Animator>();

        Subscribe();
    }

    private void OnDestroy()
    {
        UnSubscribe();
    }

    public void UpdateRunVelocity(float velocityValue)
    {
        animator.SetFloat(velocity, velocityValue);
    }

    public void StopRunning()
    {
        animator.SetFloat(velocity, 0);
        animator.Play(StopRunningAnimation);
    }

    private void Subscribe()
    {
        sentinel.OnViewExit += StopRunning;
        sentinel.OnVelocityChanged += UpdateRunVelocity;
    }

    private void UnSubscribe()
    {
        sentinel.OnViewExit -= StopRunning;
        sentinel.OnVelocityChanged -= UpdateRunVelocity;
    }
}
