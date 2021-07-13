using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sentinel : Enemy
{
    [Header("Sentinel Component")]

    public Action<float> OnVelocityChanged;
    public Action OnViewExit;

    [SerializeField] private Transform detectionConeStartPoint = null;
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private float detectionRadius = 3;

    private bool wasInViewInPrevTick = false;

    private const float viewAngle = 360;

    protected override void Awake()
    {
        base.Awake();

        playerTransform = FindObjectOfType<PlayerController>().transform;
    }

    protected override void Update()
    {
        base.Update();

        if (IsInSight())
        {
            LookAtPlayer();
            FollowToPlyer();
            CalculateNewVelocity();
        }
        CheckOnSightExit();
    }

    private bool IsInSight()
    {
        RaycastHit hit;
        var angleBetweenEyeVectorAndPlayer = Vector3.Angle(detectionConeStartPoint.position, playerTransform.position - detectionConeStartPoint.position);
        if (Physics.Raycast(detectionConeStartPoint.position, playerTransform.position - detectionConeStartPoint.position, out hit, detectionRadius))
        {
            if (angleBetweenEyeVectorAndPlayer < viewAngle
             && Vector3.Distance(detectionConeStartPoint.position, playerTransform.position) <= detectionRadius
             && hit.transform == playerTransform)
            {
                wasInViewInPrevTick = true;
                return true;
            }
        }
        return false;
    }

    private void LookAtPlayer()
    {
        transform.LookAt(playerTransform);
    }

    private void CalculateNewVelocity()
    {
        var distance = Vector3.Distance(transform.position, playerTransform.position);
        var velocity = distance / detectionRadius * 2;

        OnVelocityChanged?.Invoke(velocity);
    }

    private void FollowToPlyer()
    {
        transform.Translate(0.0f, 0.0f, enemySpeed * Time.deltaTime);
    }

    private void CheckOnSightExit()
    {
        if (!IsInSight() && wasInViewInPrevTick)
        {
            OnViewExit?.Invoke();
            wasInViewInPrevTick = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, detectionRadius);
    }
}