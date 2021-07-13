using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionHandler : MonoBehaviour
{
    public Action OnEnemyCollision;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent<Enemy>(out Enemy enemy))
        {
            DisablePlayerInteractivity();
            ChangePlayerLayer();
            OnEnemyCollision?.Invoke();
        }
    }

    private void DisablePlayerInteractivity()
    {
        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerAnimations>().enabled = false;
    }

    private void ChangePlayerLayer()
    {
        gameObject.layer = 9;
    }
}