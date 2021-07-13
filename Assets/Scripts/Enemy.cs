using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Base Enemy Component")]
    [SerializeField] protected float enemySpeed;

    protected virtual void Awake() { }
    protected virtual void Update() { }
    protected virtual void FixedUpdate() { }
}
