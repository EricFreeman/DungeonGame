using System.Collections.Generic;
using Assets.Scripts.Enemy;
using Assets.Scripts.People;
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
        private UnityEngine.Camera _viewCamera;

        void Start()
        {
            _ammo = FindObjectOfType<PlayerAmmo>();
            _viewCamera = GameObject.Find("HeadCamera").GetComponent<UnityEngine.Camera>();
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
            var ray = _viewCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && Vector3.Distance(transform.position, hit.point) < .5f)
            {
                if (hit.collider.tag == "Enemy")
                {
                    var hitContext = new HitContext
                    {
                        Damage = Random.Range(0, 2),
                        Direction = transform.forward,
                        Force = 1
                    };
                    hit.collider.GetComponent<HealthBehavior>().TakeDamage(hitContext);
                }
            }
        }

        public void RangedAttack()
        {
            if (Squirt.Count > 0)
            {
                AudioSource.PlayClipAtPoint(Squirt.Random(), transform.position);
            }

            var bullet = Instantiate(Bullet);

            if (Tip != null)
            {
                var ray = _viewCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
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