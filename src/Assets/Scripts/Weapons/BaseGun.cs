using System.Collections.Generic;
using Assets.Scripts.Enemy;
using Assets.Scripts.Player;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class BaseGun : MonoBehaviour
    {
        public AmmoType AmmunitionType;
        public GameObject Bullet;
        public bool IsMelee;
        public Transform Tip;
        public List<AudioClip> Squirt;
        public float ShotDelay = .1f;

        private float _lastShot;
        private PlayerAmmo _ammo;

        void Start()
        {
            _ammo = FindObjectOfType<PlayerAmmo>();
        }

        public void Fire()
        {
            if (CanFire())
            {
                GetComponent<Animator>().SetTrigger("Fire");

                if (IsMelee)
                {
                    MeleeAttack();
                }
                else
                {
                    RangedAttack();
                }
            }
        }

        public void MeleeAttack()
        {
            
        }

        public void RangedAttack()
        {
            AudioSource.PlayClipAtPoint(Squirt.Random(), transform.position);

            var bullet = Instantiate(Bullet);

            if (Tip != null)
            {
                var ray = UnityEngine.Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;
                Vector3 dir;

                if (Physics.Raycast(ray, out hit))
                {
                    dir = (hit.point - Tip.transform.position).normalized;
                }
                else
                {
                    dir = ray.direction;
                }

                bullet.transform.position = Tip.transform.position;
                bullet.transform.rotation = Quaternion.LookRotation(dir);
            }
            else
            {
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
            }

            bullet.GetComponent<Bullet>().Damage = Random.Range(1, 5);

            _lastShot = Time.fixedTime;
            _ammo.RemoveAmmo(AmmoType.Solution, 1);
        }

        private bool CanFire()
        {
            var ammunitionIsNotEmpty = (AmmunitionType == AmmoType.Infinite || _ammo.HasAmmo(AmmunitionType, 1));
            var itHasBeenLongEnoughSinceTheLastShot = Time.fixedTime - _lastShot > ShotDelay;

            return ammunitionIsNotEmpty && itHasBeenLongEnoughSinceTheLastShot;
        }
    }
}