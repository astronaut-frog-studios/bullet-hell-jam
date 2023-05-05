using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private Transform cameraTransform;

        private Vector2 moveDirection;
        private new Rigidbody rigidbody;

        public void OnMove(InputAction.CallbackContext context)
        {
            moveDirection = context.ReadValue<Vector2>();
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
            MoveTowards();
        }

        private void MoveTowards()
        {
            var speed = moveSpeed * Time.fixedDeltaTime;
            var targetDirection = new Vector3(moveDirection.x, 0, moveDirection.y);
            var movement = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0) * targetDirection;
           
            rigidbody.MovePosition(rigidbody.position + movement * speed);
            RotateTowardsMovementVector(targetDirection);
        }

        private void RotateTowardsMovementVector(Vector3 movementVector)
        {
            if (movementVector.magnitude == 0) return;

            var rotation = Quaternion.LookRotation(movementVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
        }
    }
}