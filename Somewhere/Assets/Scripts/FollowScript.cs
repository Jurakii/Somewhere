using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public Transform target;
    public Vector3 cameraOffset;
    public float followSpeed = 10f;
    public float xMin = 0f;
    public float maxX = float.MaxValue; // Add the maxX variable here
    Vector3 velocity = Vector3.zero;
    public bool isLocked = false;

    void FixedUpdate()
    {
        if (!isLocked)
        {
            Vector3 targetPos = target.position + cameraOffset;
            Vector3 clampedPos = new Vector3(Mathf.Clamp(targetPos.x, xMin, maxX), targetPos.y, targetPos.z); // Use maxX for clamping
            Vector3 smoothPos = Vector3.SmoothDamp(transform.position, clampedPos, ref velocity, followSpeed * Time.fixedDeltaTime);

            transform.position = smoothPos;
        }
    }
}
