using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMovement : MonoBehaviour
{
    [SerializeField] private float xMovementValue = 0.005f;
    void FixedUpdate()
    {
        MoveDummyByX();
    }

    private void MoveDummyByX()
    {
        transform.position += new Vector3(xMovementValue, 0, 0);

    }
}
