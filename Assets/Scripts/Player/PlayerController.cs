using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public bool isInvulnerable;
        [Header("Movement")] [SerializeField] private float moveSpeed;
        [SerializeField] private Camera mainCamera;

        private Vector2 moveInput, mouseLook, joystickLook;
        [HideInInspector] public Vector3 aimTarget, rotationTarget,targetDirection, aimDirection;
        private new Rigidbody rigidbody;

        [Header("DashArea")] [SerializeField] private InputActionReference dash;
        [SerializeField] private Slider dashCooldownSlider;
        [SerializeField] private float dashTime = 0.15f;
        [SerializeField] private float dashSpeed;
        [SerializeField] private float maxDashCooldown;
        [SerializeField, ReadOnly] private float dashCooldown;
        [SerializeField] private ParticleSystem dashEffect;
        private bool isDashing;

        public void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }

        public void OnMouseLook(InputAction.CallbackContext context)
        {
            mouseLook = context.ReadValue<Vector2>();
        }

        public void OnJoystickLook(InputAction.CallbackContext context)
        {
            joystickLook = context.ReadValue<Vector2>();
        }

        private void OnEnable()
        {
            dash.action.performed += PerformDash;
        }

        private void OnDisable()
        {
            dash.action.performed -= PerformDash;
        }

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            dashCooldown = maxDashCooldown;
        }

        private void Update()
        {
            Debug.DrawRay(transform.position, transform.forward * dashSpeed, Color.green);
            if (!CanResetDashCooldown)
            {
                return;
            }

            dashCooldown += Time.deltaTime;
            dashCooldownSlider.value = dashCooldown;
        }

        private void FixedUpdate()
        {
            if (isDashing) return;

            if (GameManager.Instance.isKeyboardAndMouse)
            {
                var ray = mainCamera.ScreenPointToRay(mouseLook);
                if (Physics.Raycast(ray, out var hit))
                {
                    rotationTarget = hit.point;
                    rotationTarget = hit.point - transform.position;
                    rotationTarget.y = 0;
                }

                MoveTowardsAim();
                return;
            }

            if (joystickLook.magnitude == 0)
            {
                MoveTowards();
                return;
            }

            MoveTowardsAim();
        }

        private void MoveTowards()
        {
            targetDirection = new Vector3(moveInput.x, 0, moveInput.y);
            var movement = Quaternion.Euler(0, mainCamera.transform.eulerAngles.y, 0) * targetDirection;

            rigidbody.MovePosition(rigidbody.position + movement * Speed);
            RotateTowardsMovementVector(targetDirection);
            aimDirection = targetDirection;
        }

        private void RotateTowardsMovementVector(Vector3 movementVector)
        {
            if (movementVector.magnitude == 0) return;

            var rotation = Quaternion.LookRotation(movementVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
        }

        private void MoveTowardsAim()
        {
            if (GameManager.Instance.isKeyboardAndMouse)
            {
                var rotation = Quaternion.LookRotation(rotationTarget);

                aimDirection = new Vector3(rotationTarget.x, 0f, rotationTarget.z);
                if (aimDirection.magnitude == 0) return;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
            }
            else
            {
                aimDirection = new Vector3(joystickLook.x, 0f, joystickLook.y * 100f);
                print(aimDirection);
                var rotation = Quaternion.LookRotation(aimDirection);
                if (aimDirection.magnitude == 0) return;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
            }

            targetDirection = new Vector3(moveInput.x, 0, moveInput.y);
            rigidbody.MovePosition(rigidbody.position + targetDirection * Speed);            
        }

        private void PerformDash(InputAction.CallbackContext context)
        {
            if (!CanDash) return;
            StartCoroutine(DashRoutine());
        }

        private IEnumerator DashRoutine()
        {
            dashCooldownSlider.gameObject.SetActive(true);
            dashCooldown = 0f;
            dashCooldownSlider.value = 0f;
            dashEffect.Play();
            isInvulnerable = true;

            var dashDirection = DashingDirection;
            rigidbody.velocity = dashDirection * dashSpeed;
            yield return new WaitForSeconds(dashTime);

            rigidbody.velocity = Vector3.zero;
            isInvulnerable = false;
            isDashing = false;
            dashEffect.Stop();

            yield return new WaitUntil(() => dashCooldown >= maxDashCooldown);
            dashCooldownSlider.gameObject.SetActive(false);
        }

        private Vector3 DashingDirection => aimDirection.magnitude == 0 ? targetDirection : transform.forward;
        private bool CanResetDashCooldown => dashCooldown < maxDashCooldown && !isDashing;
        private bool CanDash => dashCooldown >= maxDashCooldown;
        private float Speed => moveSpeed * Time.fixedDeltaTime;
    }
}