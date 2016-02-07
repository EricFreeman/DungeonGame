using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraScreenGrab : MonoBehaviour
    {
        public int PixelSize = 4;
        public FilterMode FilterMode = FilterMode.Point;
        public UnityEngine.Camera[] OtherCameras;

        private Material _mat;
        Texture2D _tex;

        void Start()
        {
            var width = Screen.width / PixelSize;
            var height = Screen.height / PixelSize;
            GetComponent<UnityEngine.Camera>().pixelRect = new Rect(0, 0, width, height);

            foreach (UnityEngine.Camera otherCamera in OtherCameras)
            {
                if (otherCamera.name == "UiCamera")
                {
                    otherCamera.pixelRect = new Rect(0, 0, Screen.width, Screen.height);
                }
                else
                {
                    otherCamera.pixelRect = new Rect(0, 0, width, height);
                }
            }
        }

        void OnGUI()
        {
            if (Event.current.type == EventType.Repaint)
            {
                Graphics.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _tex);
            }

            foreach (UnityEngine.Camera otherCamera in OtherCameras)
            {
                if (otherCamera.name == "UiCamera")
                {
                    otherCamera.Render();
                }
            }
        }


        void OnPostRender()
        {
            if (!_mat)
            {
                _mat = new Material("Shader \"Hidden/SetAlpha\" {" +
                                   "SubShader {" +
                                   "	Pass {" +
                                   "		ZTest Always Cull Off ZWrite Off" +
                                   "		ColorMask A" +
                                   "		Color (1,1,1,1)" +
                                   "	}" +
                                   "}" +
                                   "}"
                    );
            }
            // Draw a quad over the whole screen with the above shader
            GL.PushMatrix();
            GL.LoadOrtho();
            for (var i = 0; i < _mat.passCount; ++i)
            {
                _mat.SetPass(i);
                GL.Begin(GL.QUADS);
                GL.Vertex3(0, 0, 0.1f);
                GL.Vertex3(1, 0, 0.1f);
                GL.Vertex3(1, 1, 0.1f);
                GL.Vertex3(0, 1, 0.1f);
                GL.End();
            }
            GL.PopMatrix();


            DestroyImmediate(_tex);

            _tex = new Texture2D(Mathf.FloorToInt(GetComponent<UnityEngine.Camera>().pixelWidth), Mathf.FloorToInt(GetComponent<UnityEngine.Camera>().pixelHeight));
            _tex.filterMode = FilterMode;
            _tex.ReadPixels(new Rect(0, 0, GetComponent<UnityEngine.Camera>().pixelWidth, GetComponent<UnityEngine.Camera>().pixelHeight), 0, 0);
            _tex.Apply();
        }
    }
}