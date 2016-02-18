using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public int Health;
        public Text HealthTextBox;

        void Update()
        {
            HealthTextBox.text = "Health: " + Health;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }
    }
}
