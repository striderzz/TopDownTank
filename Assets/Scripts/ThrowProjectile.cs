using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [Tooltip("Drag your Rigidbody‐based projectile prefab here.")]
    public Rigidbody projectilePrefab;

    [Tooltip("Where the projectile will be spawned from.")]
    public Transform throwPoint;

    [Tooltip("Initial speed applied to the projectile.")]
    public float launchForce = 15f;

    [Header("Fire Rate")]
    [Tooltip("Minimum time (in seconds) between consecutive shots.")]
    public float timeBetweenShots = 0.5f;

    // Time at which we’re next allowed to shoot
    private float nextTimeToFire = 0f;

    void Update()
    {
        // On left mouse‐button down, check cooldown
        if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire)
        {
            ThrowProjectile1();
            nextTimeToFire = Time.time + timeBetweenShots;
        }
    }

    private void ThrowProjectile1()
    {
        if (projectilePrefab == null || throwPoint == null)
        {
            Debug.LogWarning("ProjectileThrower: Assign 'projectilePrefab' and 'throwPoint' in the Inspector.");
            return;
        }

        // Instantiate a new Rigidbody at the throwPoint’s position & rotation
        Rigidbody projInstance = Instantiate(
            projectilePrefab,
            throwPoint.position,
            throwPoint.rotation
        );

        // Immediately give it velocity forward based on throwPoint’s forward vector
        projInstance.velocity = throwPoint.forward * launchForce;
    }
}
