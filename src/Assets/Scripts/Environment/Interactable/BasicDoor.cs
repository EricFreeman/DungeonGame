using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Environment.Interactable
{
    public class BasicDoor : MonoBehaviour, IInteractable
    {
        public float Speed = .01f;
        public float MoveAmount = 1.1f;
        public string RequiredKey;
        public int RedDoorAttempts = 0;
        public AudioClip Error;
        public AudioClip Open;
        public AudioClip KeyCardMessage;

        private bool _wasUsed;
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private AudioSource _keyCardMessageAudioSource;

        void Start()
        {
            _keyCardMessageAudioSource = gameObject.AddComponent<AudioSource>();
            _keyCardMessageAudioSource.clip = KeyCardMessage;
        }

        void Update()
        {
            if (_wasUsed)
            {
                transform.position = Vector3.MoveTowards(transform.position, _endPosition, Speed * Time.deltaTime);
            }
        }

        public void Interact()
        {
            if (CanUse())
            {
                if (!_wasUsed)
                {
                    _wasUsed = true;
                    _startPosition = transform.position;
                    _endPosition = _startPosition + transform.right * MoveAmount;
                    AudioSource.PlayClipAtPoint(Open, transform.position);
                }
            }
            else {
                AudioSource.PlayClipAtPoint(Error, transform.position);

                RedDoorAttempts++;
                if (RedDoorAttempts > 2 && !_keyCardMessageAudioSource.isPlaying)
                {
                    _keyCardMessageAudioSource.PlayDelayed(0.2f);
                }
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