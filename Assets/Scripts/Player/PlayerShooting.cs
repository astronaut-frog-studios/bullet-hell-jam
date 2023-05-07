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

        private PlayerController playerController;

        private int currentAmmo;

        private void Start()
        {
            currentAmmo = maxAmmo;
            playerController = GetComponent<PlayerController>();
        }

        private void PerformShoot(InputAction.CallbackContext context)
        {
            // if(currentAmmo <= 0) return;
            
            var bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            var bulletScript = bullet.GetComponent<Bullet>();

            bulletScript.bulletSpeed = bulletSpeed;
            bulletScript.target = new Vector3(playerController.rotationTarget.x, firePoint.position.y, playerController.rotationTarget.z);
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