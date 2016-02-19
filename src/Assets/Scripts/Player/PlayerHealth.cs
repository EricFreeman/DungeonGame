using Assets.Scripts.People;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageBehavior
    {
        public Text HealthTextBox;
        private HealthBehavior _healthBehavior;

        void Start()
        {
            _healthBehavior = GetComponent<HealthBehavior>();
        }

        void Update()
        {
            HealthTextBox.text = "Health: " + _healthBehavior.Health;
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
            rigidBody.AddExplosionForce(hitContext.Force * 2, transform.position - hitContext.Direction, 1f, 1f, ForceMode.Impulse);

            GetComponent<CapsuleCollider>().height = .15f;
            GetComponent<CapsuleCollider>().radius = .15f;
            Destroy(GetComponentInChildren<BoxCollider>()); 

            Destroy(GetComponentInChildren<MouseRotationX>());
            Destroy(GetComponentInChildren<MouseRotationY>());
            Destroy(GetComponentInChildren<KeyboardMovement>());
            Destroy(GetComponentInChildren<Bobber>());
            Destroy(GetComponentInChildren<Tilter>());
            Destroy(GetComponentInChildren<PlayerInteractions>());
            Destroy(gameObject.GetComponent<PlayerGun>());
        }
    }
}