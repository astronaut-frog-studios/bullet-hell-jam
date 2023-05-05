using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private InputActionReference shoot;

        [Header("Snowball")]
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private int maxAmmo;

        private int currentAmmo;

        private void Start()
        {
            currentAmmo = maxAmmo;
        }

        private void PerformShoot(InputAction.CallbackContext context)
        {
            // if(currentAmmo <= 0) return;
            
            var bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            var bulletRigidbody = bullet.GetComponent<Rigidbody>();

            bulletRigidbody.velocity = bulletSpeed * transform.forward;
            currentAmmo--;
        }

        private void OnEnable()
        {
            shoot.action.performed += PerformShoot;
        }

        private void OnDisable()
        {
            shoot.action.performed -= PerformShoot;
        }
    }
}