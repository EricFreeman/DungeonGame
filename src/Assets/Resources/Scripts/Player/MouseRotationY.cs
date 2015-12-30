using Assets.Resources.Scripts.Configuration;
using UnityEngine;

namespace Assets.Resources.Scripts.Player
{
    public class MouseRotationY : MonoBehaviour
    {
        private float _rotation;

        void FixedUpdate()
        {
            var verticalTurn = Input.GetAxis("Mouse Y") * MouseConfiguration.MouseSensetivity * Time.deltaTime * -1;
            _rotation += verticalTurn;
            _rotation = Mathf.Clamp(_rotation, -90, 90);
            transform.localRotation = Quaternion.AngleAxis(_rotation, Vector3.right);
        }
    }
}