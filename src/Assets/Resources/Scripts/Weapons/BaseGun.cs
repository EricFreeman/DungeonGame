using UnityEngine;

namespace Assets.Resources.Scripts.Weapons
{
    public class BaseGun : MonoBehaviour
    {
        public int Ammunition;
        public GameObject Bullet;
        public GameObject Tip;

        public float ShotDelay = .1f;
        private float _lastShot;

        public void Fire()
        {
            if (CanFire())
            {
                var bullet = Instantiate(Bullet);

                if (Tip != null)
                {
                    bullet.transform.position = Tip.transform.position;
                    bullet.transform.rotation = Tip.transform.rotation;
                }
                else
                {
                    bullet.transform.position = transform.position;
                    bullet.transform.rotation = transform.rotation;
                }

                bullet.GetComponent<Bullet>().Damage = Random.Range(1, 5);

                _lastShot = Time.fixedTime;

                Ammunition--;
            }
        }

        private bool CanFire()
        {
            var ammunitionIsNotEmpty = Ammunition > 0;
            var itHasBeenLongEnoughSinceTheLastShot = Time.fixedTime - _lastShot > ShotDelay;

            return ammunitionIsNotEmpty && itHasBeenLongEnoughSinceTheLastShot;
        }
    }
}