using Assets.Resources.Scripts.Gore;
using Assets.Resources.Scripts.People;
using UnityEngine;

namespace Assets.Resources.Scripts.Enemy
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
