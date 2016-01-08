using Assets.Resources.Scripts.Utils;
using UnityEngine;

namespace Assets.Resources.Scripts.Player
{
    public class KeyboardMovement : MonoBehaviour
    {
        public float MoveSpeed = 3.5f;
        public float Acceleration = 1f;

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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Rigidbody>().AddForce(0, 100, 0);
            }
        }
    }
}