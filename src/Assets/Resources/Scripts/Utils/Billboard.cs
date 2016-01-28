using UnityEngine;

namespace Assets.Resources.Scripts.Utils
{
    public class Billboard : MonoBehaviour
    {
        private void Update()
        {
////            var x = transform.localRotation.x;
//            transform.LookAt(UnityEngine.Camera.main.transform.position);
//            transform.rotation = new Quaternion(0f, transform.rotation.y, 0f, 0f);

            Vector3 v = UnityEngine.Camera.main.transform.position - transform.position;
            v.x = v.z = 0.0f;
            transform.LookAt(UnityEngine.Camera.main.transform.position - v);
        }
    }
}