using UnityEngine;

namespace Assets.Resources.Scripts.Gore
{
    public class BloodEjector : MonoBehaviour
    {
        public GameObject BloodDecal;
        public float MaxForce;

        private GameObject _player;

        void Start()
        {
            _player = GameObject.Find("Player");
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                for (var i = 0; i < 3; i++)
                {
                    var blood = Instantiate(BloodDecal);
                    blood.transform.position = transform.position;
                    blood.transform.Translate(Random.Range(-MaxForce, MaxForce), Random.Range(-MaxForce, MaxForce), Random.Range(-MaxForce, MaxForce));
                }
            }
        }
    }
}