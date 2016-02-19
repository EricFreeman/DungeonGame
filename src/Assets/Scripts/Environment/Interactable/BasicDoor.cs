using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Environment.Interactable
{
    public class BasicDoor : MonoBehaviour, IInteractable
    {
        public float Speed = .01f;
        public float MoveAmount = 1.1f;
        public string RequiredKey;

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

        public void Interact()
        {
            if (CanUse() && !_wasUsed)
            {
                _wasUsed = true;
                _startPosition = transform.position;
                _endPosition = _startPosition + transform.right * MoveAmount;
            }
        }

        private bool CanUse()
        {
            if (string.IsNullOrEmpty(RequiredKey) || PlayerInventory.HasItem(RequiredKey))
            {
                return true;
            }

            return false;
        }
    }
}