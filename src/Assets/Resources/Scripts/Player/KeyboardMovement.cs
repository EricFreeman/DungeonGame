using System.Security.Permissions;
using Assets.Resources.Scripts.Utils;
using UnityEngine;

namespace Assets.Resources.Scripts.Player
{
    public class KeyboardMovement : MonoBehaviour
    {
        public float MoveSpeed = 3.5f;
        public float Acceleration = 1f;
        public float JumpForce = 200f;

        private float _currentMoveSpeed;
        private Vector3 _lastDirection;

        void FixedUpdate()
        {
            var horizontalMovement = Input.GetAxisRaw("Horizontal");
            var verticalMovement = Input.GetAxisRaw("Vertical");

            var moveVector = new Vector3(horizontalMovement, 0, verticalMovement);

            var magnitude = moveVector.magnitude > 1 ? 1 : moveVector.magnitude;
            var speed = magnitude * MoveSpeed;
            _currentMoveSpeed = _currentMoveSpeed.MoveTowards(speed, Acceleration);

            if (speed > 0)
            {
                _lastDirection = moveVector;
            }

            var movement = (speed > 0 ? moveVector : _lastDirection) * _currentMoveSpeed * Time.deltaTime;

            transform.Translate(movement);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                GetComponent<Rigidbody>().AddForce(0, JumpForce, 0);
            }
        }

        private bool IsGrounded()
        {
            return Physics.Raycast(transform.position - new Vector3(0, .45f, 0), Vector3.down, .1f);
        }
    }
}