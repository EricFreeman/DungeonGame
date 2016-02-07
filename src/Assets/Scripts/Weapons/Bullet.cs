using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Bullet : MonoBehaviour {

        public float Speed;
        public int Damage = 10;

        void Update()
        {
            transform.position += (transform.forward.normalized * Speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.tag != "Player")
            {
                Destroy(gameObject);
            }
        }
    }
}
