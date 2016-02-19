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
            // TODO: Kill player here
        }
    }
}
