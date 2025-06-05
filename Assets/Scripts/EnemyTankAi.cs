using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankAI : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Assign the player's Transform here.")]
    public Transform playerTransform;

    [Header("Range Settings")]
    [Tooltip("Distance at which the tank will start following the player.")]
    public float followRange = 30f;

    [Tooltip("Distance within which the tank will stop chasing and start shooting.")]
    public float minDistance = 10f;

    [Header("Movement Settings")]
    [Tooltip("Speed at which the tank moves toward the player.")]
    public float tankSpeed = 15f;

    [Tooltip("Rotation speed (degrees/sec) when turning the tank body.")]
    public float tankRotationSpeed = 20f;

    private Rigidbody rb;

    [Header("Turret Settings")]
    [Tooltip("The turret (child Transform) that should track the player.")]
    public Transform turretTransform;

    [Tooltip("How quickly the turret “lags” behind when turning (higher = snappier).")]
    public float turretLagSpeed = 6f;

    // Internally stores the smoothed look‐direction for the turret
    private Vector3 finalTurretLookDir;

    [Header("Projectile Settings")]
    [Tooltip("Drag your Rigidbody‐based projectile prefab here.")]
    public Rigidbody projectilePrefab;

    [Tooltip("Where the projectile will be spawned from (e.g. turret barrel tip).")]
    public Transform throwPoint;

    [Tooltip("Initial speed applied to the projectile.")]
    public float launchForce = 15f;

    [Header("Fire Rate")]
    [Tooltip("Minimum time (in seconds) between consecutive shots.")]
    public float timeBetweenShots = 0.5f;

    // Time at which we’re next allowed to shoot
    private float nextTimeToFire = 0f;


    void Start()
    {
        playerTransform = FindFirstObjectByType<TankController>().transform;
        rb = GetComponent<Rigidbody>();

        if (turretTransform != null)
        {
            // Initialize the turret’s look direction so it starts pointing forward
            finalTurretLookDir = turretTransform.forward;
        }
    }

    void FixedUpdate()
    {
        if (rb == null || playerTransform == null) return;

        HandleMovement();
        HandleTurret();
        HandleShooting();
    }

    /// <summary>
    /// Two‐state logic for the tank:
    /// 1) If the player is outside followRange → Idle (no movement/rotation).
    /// 2) If the player is within followRange but outside minDistance → Follow (rotate + move).
    /// 3) If the player is within minDistance → Stop chasing (rotate only).
    /// </summary>
    private void HandleMovement()
    {
        Vector3 toPlayer = playerTransform.position - transform.position;
        toPlayer.y = 0f;
        float distance = toPlayer.magnitude;

        // State 1: Player too far → Idle
        if (distance > followRange)
        {
            return;
        }

        // State 2: Within followRange but beyond minDistance → Follow
        if (distance > minDistance)
        {
            // Rotate body toward player
            if (toPlayer.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(toPlayer.normalized);
                Quaternion newRot = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRot,
                    tankRotationSpeed * Time.deltaTime
                );
                rb.MoveRotation(newRot);
            }

            // Move forward
            Vector3 forwardMotion = transform.forward * tankSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + forwardMotion);
        }
        else
        {
            // State 3: Within minDistance → Stop chasing but still turn to face player
            if (toPlayer.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(toPlayer.normalized);
                Quaternion newRot = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRot,
                    tankRotationSpeed * Time.deltaTime
                );
                rb.MoveRotation(newRot);
            }
            // No forward movement
        }
    }

    /// <summary>
    /// Turret always tracks the player with a slight smoothing (lag).
    /// </summary>
    private void HandleTurret()
    {
        if (turretTransform == null) return;

        Vector3 turretDir = playerTransform.position - turretTransform.position;
        turretDir.y = 0f;

        if (turretDir.sqrMagnitude < 0.001f) return;

        finalTurretLookDir = Vector3.Lerp(
            finalTurretLookDir,
            turretDir.normalized,
            Time.deltaTime * turretLagSpeed
        );
        turretTransform.rotation = Quaternion.LookRotation(finalTurretLookDir);
    }

    /// <summary>
    /// When the player is within minDistance, fire a projectile at fixed intervals.
    /// </summary>
    private void HandleShooting()
    {
        if (projectilePrefab == null || throwPoint == null) return;

        Vector3 toPlayer = playerTransform.position - transform.position;
        toPlayer.y = 0f;
        float distance = toPlayer.magnitude;
        Debug.Log(distance);

        // Only shoot when inside minDistance but still inside followRange
        if (distance <= minDistance && distance <= followRange)
        {
            if (Time.time >= nextTimeToFire)
            {
                ThrowProjectile1();
                nextTimeToFire = Time.time + timeBetweenShots;
            }
        }
    }

    /// <summary>
    /// Instantiates a new projectile and gives it initial velocity forward from throwPoint.
    /// </summary>
    private void ThrowProjectile1()
    {
        if (projectilePrefab == null || throwPoint == null)
        {
            Debug.LogWarning("EnemyTankAI: Assign 'projectilePrefab' and 'throwPoint' in the Inspector.");
            return;
        }

        // Instantiate a new Rigidbody at the throwPoint's position & rotation
        Rigidbody projInstance = Instantiate(
            projectilePrefab,
            throwPoint.position,
            throwPoint.rotation
        );

        // Immediately give it velocity forward based on throwPoint's forward vector
        projInstance.velocity = throwPoint.forward * launchForce;
    }
}
