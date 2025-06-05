using UnityEngine;

public class TopDownCameraFollow : MonoBehaviour
{
    [Header("Target to Follow")]
    public Transform target;            // Drag your player (tank, character, etc.) here

    [Header("Camera Offset")]
    public Vector3 offset = new Vector3(0f, 10f, 0f);
    // By default, this puts the camera 10 units above the player on Y.

    [Header("Rotation Offset")]
    public float RotationX;
    public float RotationY;
    public float RotationZ;
    // By default, this puts the camera 10 units above the player on Y.

    [Header("Optional Smoothing")]
    [Tooltip("If zero or negative, camera will snap directly. Higher = slower follow.")]
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // Desired camera position: player's position + offset
        Vector3 desiredPos = target.position + offset;

        if (smoothSpeed > 0f)
        {
            // Smoothly interpolate from current to desired
            transform.position = Vector3.Lerp(
                transform.position,
                desiredPos,
                smoothSpeed * Time.deltaTime
            );
        }
        else
        {
            // No smoothing → snap immediately
            transform.position = desiredPos;
        }

        // Make sure the camera is looking straight down at the player
        // (Assumes offset.y > 0 and you want angle = 90°).
        transform.rotation = Quaternion.Euler(RotationX, RotationY, RotationZ);
    }
}
