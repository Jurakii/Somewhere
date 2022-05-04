using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public Transform target;
    public Vector3 cameraOffset;
    public float followSpeed = 10f;
    public float xMin = 0f;
    Vector3 velocity = Vector3.zero;
    public bool isLocked = false;

    void FixedUpdate()
    {
        if (!isLocked)
        {
        Vector3 targetPos = target.position + cameraOffset;
        Vector3 clampedPos = new Vector3(Mathf.Clamp(targetPos.x, xMin, float.MaxValue), targetPos.y, targetPos.z);
        Vector3 smoothPos = Vector3.SmoothDamp(transform.position, clampedPos, ref velocity, followSpeed * Time.fixedDeltaTime);

        transform.position = smoothPos;
        }
    }
}