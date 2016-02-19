using Assets.Scripts.People;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageBehavior
    {
        public Text HealthTextBox;
        public Image HealthImage;
        public GameObject Hand;

        private HealthBehavior _healthBehavior;

        void Start()
        {
            _healthBehavior = GetComponent<HealthBehavior>();
        }

        void Update()
        {
            HealthTextBox.text = "Health: " + _healthBehavior.Health;
            HealthImage.color = new Color(255, 0, 0, 1 - _healthBehavior.Health / 100f);
        }

        public void TakeDamage(int damage)
        {
            _healthBehavior.Health -= damage;
        }

        public void OnHit(HitContext hitContext)
        {
            // TODO: Flash screen red or some shit here
        }

        public void OnDeath(HitContext hitContext)
        {
            var rigidBody = GetComponent<Rigidbody>();
            rigidBody.constraints = RigidbodyConstraints.None;
            rigidBody.AddExplosionForce(hitContext.Force * 2, transform.position - hitContext.Direction, 1f, 3f, ForceMode.Impulse);

            gameObject.GetComponent<BoxCollider>().center = Vector3.zero;
            gameObject.GetComponent<BoxCollider>().size = new Vector3(.4f,.3f, .27f);

            Destroy(GetComponent<CapsuleCollider>());
            Destroy(GetComponentInChildren<MouseRotationX>());
            Destroy(GetComponentInChildren<MouseRotationY>());
            Destroy(GetComponentInChildren<KeyboardMovement>());
            Destroy(GetComponentInChildren<Bobber>());
            Destroy(GetComponentInChildren<Tilter>());
            Destroy(GetComponentInChildren<PlayerInteractions>());
            Destroy(gameObject.GetComponent<PlayerGun>());
            Destroy(Hand);
        }
    }
}