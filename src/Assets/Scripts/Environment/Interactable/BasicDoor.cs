using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Environment.Interactable
{
    public class BasicDoor : MonoBehaviour, IInteractable
    {
        public float Speed = .01f;
        public string RequiredKey;

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
            if (CanUse() && !_wasUsed)
            {
                _wasUsed = true;
                _startPosition = transform.position;
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