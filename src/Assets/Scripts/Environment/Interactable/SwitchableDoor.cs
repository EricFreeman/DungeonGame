using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Environment.Interactable
{
    public class SwitchableDoor : MonoBehaviour, ISwitchable
    {
        public float Speed = 0.1f;
        public float MoveAmount = 1.1f;
        public AudioClip Open;

        private bool _wasUsed;
        private Vector3 _startPosition;
        private Vector3 _endPosition;

        void Update()
        {
            if (_wasUsed)
            {
                transform.position = Vector3.MoveTowards(transform.position, _endPosition, Speed);
            }
        }

        public void Switch()
        {
            if(!_wasUsed)
            {
                _wasUsed = true;
                _startPosition = transform.position;
                _endPosition = _startPosition + transform.right * MoveAmount;
                AudioSource.PlayClipAtPoint(Open, transform.position);
            }
        }
    }
}