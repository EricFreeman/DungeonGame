using UnityEngine;

namespace Assets.Resources.Scripts.Weapons
{
    public class TurnSway : MonoBehaviour
    {
        public float MoveAmount = .5f;
        public float MoveSpeed = 20f;

        void Update ()
        {
            var defaultPos = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
            var moveOnX = -Input.GetAxis("Mouse X") * Time.deltaTime * MoveAmount;
            var newGunPos = new Vector3(defaultPos.x + moveOnX, defaultPos.y, defaultPos.z);

            transform.localPosition = Vector3.Lerp(transform.localPosition, newGunPos, MoveSpeed * Time.deltaTime);
        }
    }
}
