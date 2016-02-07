using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class BaseGun : MonoBehaviour
    {
        public int Ammunition;
        public GameObject Bullet;
        public Transform Tip;

        public float ShotDelay = .1f;
        private float _lastShot;

        public void Fire()
        {
            if (CanFire())
            {
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