using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel = null;

    private CollisionHandler playerCollisionHandler;

    private void Awake()
    {
        playerCollisionHandler = FindObjectOfType<CollisionHandler>();

        Subscribe();
    }

    private void OnDestroy()
    {
        UnSubscribe();
    }

    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    private void Subscribe()
    {
        playerCollisionHandler.OnEnemyCollision += ShowGameOverPanel;
    }

    private void UnSubscribe()
    {
        playerCollisionHandler.OnEnemyCollision -= ShowGameOverPanel;
    }
}
