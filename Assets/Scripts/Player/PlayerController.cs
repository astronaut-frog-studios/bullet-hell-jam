using System;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private Camera mainCamera;
        
        private Vector2 moveInput, mouseLook, joystickLook;
        private Vector3 rotationTarget;
        private new Rigidbody rigidbody;

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

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.isKeyboardAndMouse)
            {
                var ray = mainCamera.ScreenPointToRay(mouseLook);
                if (Physics.Raycast(ray, out var hit))
                {
                    rotationTarget = hit.point;
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
            var targetDirection = new Vector3(moveInput.x, 0, moveInput.y);
            var movement = Quaternion.Euler(0, mainCamera.transform.eulerAngles.y, 0) * targetDirection;

            rigidbody.MovePosition(rigidbody.position + movement * Speed);
            RotateTowardsMovementVector(targetDirection);
        }

        private void RotateTowardsMovementVector(Vector3 movementVector)
        {
            if (movementVector.magnitude == 0) return;

            var rotation = Quaternion.LookRotation(movementVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
        }

        private void MoveTowardsAim()
        {
            var targetDirection = new Vector3(moveInput.x, 0, moveInput.y);
            rigidbody.MovePosition(rigidbody.position + targetDirection * Speed);

            if (GameManager.Instance.isKeyboardAndMouse)
            {
                var lookPosition = rotationTarget - transform.position;
                lookPosition.y = 0f;
                var rotation = Quaternion.LookRotation(lookPosition);

                var aimDirection = new Vector3(rotationTarget.x, 0f, rotationTarget.y);
                if (aimDirection.magnitude == 0) return;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
            }
            else
            {
                var aimDirection = new Vector3(joystickLook.x, 0f, joystickLook.y);
                var rotation = Quaternion.LookRotation(aimDirection);
                if (aimDirection.magnitude == 0) return;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
            }
        }

        private float Speed => moveSpeed * Time.fixedDeltaTime;
    }
}