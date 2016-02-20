using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Environment.Interactable
{
    public class BasicSwitch : MonoBehaviour, IInteractable
    {
        public GameObject Switchable;
        public string RequiredKey;
        public bool IsOn;
        public Sprite On;
        public Sprite Off;

        public AudioClip Error;
        public AudioClip Open;

        void Start()
        {
            GetComponentInChildren<SpriteRenderer>().sprite = IsOn ? On : Off;
        }

        public void Interact()
        {
            if (CanUse())
            {
                IsOn = !IsOn;
                GetComponentInChildren<SpriteRenderer>().sprite = IsOn ? On : Off;
                Switchable.GetComponent<ISwitchable>().Switch();
                AudioSource.PlayClipAtPoint(Open, transform.position);
            }
            else {
                AudioSource.PlayClipAtPoint(Error, transform.position);
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