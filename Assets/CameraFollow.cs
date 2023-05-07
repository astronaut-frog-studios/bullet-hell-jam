using UnityEngine;

[RequireComponent(typeof(Transform))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private Vector3 offset;

    private Vector3 velocity = Vector3.zero;

    private void Update()
    {  
        var targetPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}