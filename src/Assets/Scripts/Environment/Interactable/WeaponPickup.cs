using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Environment.Interactable
{
    public class WeaponPickup : MonoBehaviour
    {
        public GameObject Sprite;
        public float OffsetY = .25f;
        public float Speed = 4;
        public float MaxSway = .125f;
        public AudioClip Take;

        public bool IsWeapon;
        public bool IsAmmo;
        public int WeaponId;
        public int AmmountCount;
        public AmmoType AmmoType;

        private float _swayRatio;

        void Start()
        {
            _swayRatio = 1 / MaxSway;
            GetComponent<SphereCollider>().center = new Vector3(0, OffsetY, 0);
        }

        void Update()
        {
            Sprite.transform.localPosition = new Vector3(0, OffsetY + Mathf.Sin(Time.fixedTime * Speed) / _swayRatio, 0);
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.name == "Player")
            {
                AudioSource.PlayClipAtPoint(Take, transform.position);

                var playerAmmo = GameObject.FindObjectOfType<PlayerAmmo>();

                if (IsWeapon)
                {
                    playerAmmo.CollectWeapon(WeaponId);
                }

                if (IsAmmo)
                {
                    playerAmmo.AddAmmo(AmmoType, AmmountCount);
                }

                Destroy(gameObject);
            }
        }
    }
}