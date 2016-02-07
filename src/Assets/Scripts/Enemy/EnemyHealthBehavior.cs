using Assets.Scripts.Gore;
using Assets.Scripts.People;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyHealthBehavior : MonoBehaviour, IDamageBehavior
    {
        public void OnHit()
        {
            var ejector = gameObject.GetComponent<BloodEjector>();
            if (ejector != null)
            {
                ejector.Eject();
            }
        }

        public void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}
