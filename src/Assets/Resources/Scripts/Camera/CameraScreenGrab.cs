using UnityEngine;

//Attach this to a camera
namespace Assets.Resources.Scripts.Camera
{
    public class CameraScreenGrab : MonoBehaviour
    {

        //how chunky to make the screen
        public int pixelSize = 4;
        public FilterMode filterMode = FilterMode.Point;
        public UnityEngine.Camera[] otherCameras;
        private Material mat;
        Texture2D tex;

        void Start()
        {
            GetComponent<UnityEngine.Camera>().pixelRect = new Rect(0, 0, Screen.width / pixelSize, Screen.height / pixelSize);
            for (int i = 0; i < otherCameras.Length; i++)
                otherCameras[i].pixelRect = new Rect(0, 0, Screen.width / pixelSize, Screen.height / pixelSize);
        }

        void OnGUI()
        {
            if (Event.current.type == EventType.Repaint)
                Graphics.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex);
        }


        void OnPostRender()
        {
            if (!mat)
            {
                mat = new Material("Shader \"Hidden/SetAlpha\" {" +
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
            for (var i = 0; i < mat.passCount; ++i)
            {
                mat.SetPass(i);
                GL.Begin(GL.QUADS);
                GL.Vertex3(0, 0, 0.1f);
                GL.Vertex3(1, 0, 0.1f);
                GL.Vertex3(1, 1, 0.1f);
                GL.Vertex3(0, 1, 0.1f);
                GL.End();
            }
            GL.PopMatrix();


            DestroyImmediate(tex);

            tex = new Texture2D(Mathf.FloorToInt(GetComponent<UnityEngine.Camera>().pixelWidth), Mathf.FloorToInt(GetComponent<UnityEngine.Camera>().pixelHeight));
            tex.filterMode = filterMode;
            tex.ReadPixels(new Rect(0, 0, GetComponent<UnityEngine.Camera>().pixelWidth, GetComponent<UnityEngine.Camera>().pixelHeight), 0, 0);
            tex.Apply();
        }

    }
}