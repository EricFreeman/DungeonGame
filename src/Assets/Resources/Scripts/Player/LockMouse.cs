using UnityEngine;

namespace Assets.Resources.Scripts.Player
{
    public class LockMouse : MonoBehaviour
    {
        void Update()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();

                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #endif
            }
        }
    }
}