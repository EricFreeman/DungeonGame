using UnityEngine;

namespace Assets.Resources.Scripts.Player
{
    public class KeyboardMovement : MonoBehaviour
    {
        public float MoveSpeed = 3.5f;

        void FixedUpdate()
        {
            var horizontalMovement = Input.GetAxisRaw("Horizontal");
            var verticalMovement = Input.GetAxisRaw("Vertical");

            var movement = new Vector3(horizontalMovement, 0, verticalMovement) * MoveSpeed * Time.deltaTime;

            transform.Translate(movement);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Rigidbody>().AddForce(0, 100, 0);
            }
        }
    }
}