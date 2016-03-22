using UnityEngine;

namespace Assets.Scripts.Gore
{
    public class BloodDroplet : MonoBehaviour
    {
        public GameObject BloodSplat;

        private float _spawnTime;
        private float _selfDestructTime = 1f;

        void Start()
        {
            _spawnTime = Time.time;
        }

        void Update()
        {
            if (Time.time - _spawnTime > _selfDestructTime)
            {
                Destroy(gameObject);
            }
        }

        void OnCollisionEnter(Collision col)
        {
            var splat = Instantiate(BloodSplat);
            splat.transform.position = col.contacts[0].point;
            splat.transform.rotation = Quaternion.FromToRotation(Vector3.up, col.contacts[0].normal);

            Destroy(gameObject);
        }
    }
}