using Assets.Scripts.Player;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyRangedAttack : MonoBehaviour
    {
        public GameObject Projectile;
        public GameObject Tip;
        public float MinDistance = .9f;
        public int Damage = 5;

        private PlayerHealth _player;

        private void Start()
        {
            _player = GameObject.Find("Player").GetComponent<PlayerHealth>();
        }

        public void Attack()
        {
            GetComponent<EnemySounds>().PlayAttackSound();  
            var projectile = Instantiate(Projectile);
            projectile.transform.position = Tip.transform.position;
            projectile.transform.LookAt(_player.transform.position);
            projectile.GetComponent<Bullet>().IsFriendly = false; 
        }
    }
}
