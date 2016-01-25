using UnityEngine;

namespace Assets.Resources.Scripts.Gore
{
    public class BloodEjector : MonoBehaviour
    {
        public GameObject BloodDroplet;

        public float MinForce;
        public float MaxForce;

        void Update()
        {
//            if (Input.GetMouseButton(0))
//            {
//                for (var i = 0; i < 3; i++)
//                {
//                    var blood = Instantiate(BloodDroplet);
//                    blood.transform.position = transform.position + new Vector3(0, .2f, 0);
//                    blood.GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * Random.Range(MinForce, MaxForce));
//                }
//            }
        }
    }
}