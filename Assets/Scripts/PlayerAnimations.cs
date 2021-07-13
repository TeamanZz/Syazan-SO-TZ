using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private PlayerController playerController;
    private CollisionHandler collisionHandler;
    private Animator animator;

    private const string deathAnimation = "Death";
    private const string velocity = "velocity";
    private const string isInAir = "isInAir";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        collisionHandler = GetComponent<CollisionHandler>();

        Subscribe();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void PlayDeathAnimation()
    {
        animator.Play(deathAnimation);
    }

    private void StopJumpAnimation()
    {
        animator.SetBool(isInAir, false);
    }

    public void UpdateRunVelocity(float velocityValue)
    {
        animator.SetFloat(velocity, velocityValue);
    }

    public void PlayJumpAnimation()
    {
        animator.SetBool(isInAir, true);
    }

    private void Subscribe()
    {
        playerController.OnVelocityChanged += UpdateRunVelocity;
        playerController.OnPlayerJump += PlayJumpAnimation;
        playerController.OnPlayerLand += StopJumpAnimation;
        collisionHandler.OnEnemyCollision += PlayDeathAnimation;
    }

    private void Unsubscribe()
    {
        playerController.OnVelocityChanged -= UpdateRunVelocity;
        playerController.OnPlayerJump -= PlayJumpAnimation;
        playerController.OnPlayerLand -= StopJumpAnimation;
        collisionHandler.OnEnemyCollision -= PlayDeathAnimation;
    }
}