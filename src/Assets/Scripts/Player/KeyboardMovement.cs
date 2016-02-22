using System.Linq;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class KeyboardMovement : MonoBehaviour
    {
        public float MoveSpeed = 3.5f;
        public float Acceleration = 1f;
        public float JumpForce = 200f;
        public float MaxGroundedJumpTime = .25f;
        private float MaxJumpDelay = .25f;

        private float _currentMoveSpeed;
        private Vector3 _lastDirection;

        private bool _isJumping;
        private bool _wasGrounded;
        private float _lastGroundedTime;

        private float _lastJumpTime;

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
            var rayDistance = .4f;
            var offset = new Vector3(0, -.2f, 0);

            if (movement.z > 0 && Physics.Raycast(transform.position + offset, transform.forward, rayDistance, 1 << 8))
            {
                movement = new Vector3(movement.x, movement.y, 0);
            }
            else if (movement.z < 0 && Physics.Raycast(transform.position + offset, -transform.forward, rayDistance, 1 << 8))
            {
                movement = new Vector3(movement.x, movement.y, 0);
            }

            if (movement.x > 0 && Physics.Raycast(transform.position + offset, transform.right, rayDistance, 1 << 8))
            {
                movement = new Vector3(0, movement.y, movement.z);
            }
            else if (movement.x < 0 && Physics.Raycast(transform.position + offset, -transform.right, rayDistance, 1 << 8))
            {
                movement = new Vector3(0, movement.y, movement.z);
            }

            transform.Translate(movement);
        }

        void Update()
        {
            if (IsGrounded())
            {
                _isJumping = false;
                _wasGrounded = true;
                _lastGroundedTime = Time.time;
            }
            else if (Time.time - _lastGroundedTime > MaxGroundedJumpTime)
            {
                _wasGrounded = false;
                _isJumping = false;
            }

            var canJump = Time.time - _lastJumpTime > MaxJumpDelay;

            if (Input.GetKeyDown(KeyCode.Space) && _wasGrounded && !_isJumping && canJump)
            {
                _isJumping = true;
                _lastJumpTime = Time.time;

                var rigidbody = GetComponent<Rigidbody>();
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
                rigidbody.AddForce(0, JumpForce, 0);
            }
        }

        private bool IsGrounded()
        {
            var size = .3f;
            var halfHeight = .45f;
            var castDistance = .15f;

            var rays = new[]
            {
                new Vector3(0, halfHeight, 0),

                new Vector3(size, halfHeight, size),
                new Vector3(-size, halfHeight, -size),
                new Vector3(-size, halfHeight, size),
                new Vector3(size, halfHeight, -size),
                new Vector3(0, halfHeight, size),
                new Vector3(0, halfHeight, -size),
                new Vector3(-size, halfHeight, 0),
                new Vector3(size, halfHeight, 0),

                new Vector3(size/2, halfHeight, size/2),
                new Vector3(-size/2, halfHeight, -size/2),
                new Vector3(-size/2, halfHeight, size/2),
                new Vector3(size/2, halfHeight, -size/2),
                new Vector3(0, halfHeight, size/2),
                new Vector3(0, halfHeight, -size/2),
                new Vector3(-size/2, halfHeight, 0),
                new Vector3(size/2, halfHeight, 0),

                new Vector3(size/3, halfHeight, size/3),
                new Vector3(-size/3, halfHeight, -size/3),
                new Vector3(-size/3, halfHeight, size/3),
                new Vector3(size/3, halfHeight, -size/3),
                new Vector3(0, halfHeight, size/3),
                new Vector3(0, halfHeight, -size/3),
                new Vector3(-size/3, halfHeight, 0),
                new Vector3(size/3, halfHeight, 0),
            };

            foreach (var ray in rays)
            {
                Debug.DrawLine(transform.position - ray, transform.position - ray - (Vector3.down * castDistance));
            }

            return rays.Any(ray => Physics.Raycast(transform.position - ray, Vector3.down, castDistance));
        }
    }
}