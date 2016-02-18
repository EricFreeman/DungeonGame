using UnityEngine;

namespace Assets.Scripts.Environment.Interactable
{
    public class BasicDoor : MonoBehaviour, IInteractable
    {
        public float Speed = .01f;

        private bool _wasUsed;
        private Vector3 _startPosition;

        void Update()
        {
            if (_wasUsed)
            {
                transform.position = Vector3.MoveTowards(transform.position, _startPosition + new Vector3(1.1f, 0, 0), Speed);
            }
        }

        public void Interact()
        {
            if (!_wasUsed)
            {
                _wasUsed = true;
                _startPosition = transform.position;
            }
        }
    }
}