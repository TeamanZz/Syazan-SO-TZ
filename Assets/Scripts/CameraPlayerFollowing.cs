using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFollowing : MonoBehaviour
{
    [SerializeField] private Transform targetTransform = null;
    [SerializeField] private Vector3 offset = default;
    [SerializeField] private float smoothSpeed = 0.125f;

    private void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 desiredPosition = targetTransform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(targetTransform);
    }
}