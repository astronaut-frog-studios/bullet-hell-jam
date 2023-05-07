using UnityEngine;

public class CanvasConstraints : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0);
        transform.localRotation = Quaternion.Euler(-90, transform.localRotation.y, 0);
    }
}