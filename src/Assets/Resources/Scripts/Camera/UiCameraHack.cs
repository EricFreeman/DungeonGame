using UnityEngine;

namespace Assets.Resources.Scripts.Camera
{
    public class UiCameraHack : MonoBehaviour
    {
        void Start()
        {
            GetComponent<UnityEngine.Camera>().depth = -999; //fuck it, it works
        }
    }
}