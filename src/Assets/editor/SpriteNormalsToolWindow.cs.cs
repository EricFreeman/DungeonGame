using UnityEngine;
using UnityEditor;
using System.IO;
using System;

namespace Editor
{
    public class SpriteNormalsToolWindow : EditorWindow
    {
        [MenuItem("Window/Sprite utils/Normal map generator")]
        public static void ShowWindow()
        {
            EditorWindow win = EditorWindow.GetWindow<SpriteNormalsToolWindow>("Sprite normals tool", true);
            win.minSize = new Vector2(100, 100);
        }

        public Texture2D horizontalNormals = null;
        public Texture2D VerticalNormals = null;
        public Texture2D OutputTexture = null;

        public void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            {
                horizontalNormals = EditorGUILayout.ObjectField("Horizontal normals", horizontalNormals, typeof(Texture2D), true) as Texture2D;
                VerticalNormals = EditorGUILayout.ObjectField("Vertical normals", VerticalNormals, typeof(Texture2D), true) as Texture2D;
                EditorGUILayout.Space();
                OutputTexture = EditorGUILayout.ObjectField("Output texture", OutputTexture, typeof(Texture2D), true) as Texture2D;
                if (horizontalNormals == null || VerticalNormals == null)
                    GUI.enabled = false;
                if (GUILayout.Button("Generate normals"))
                    GenerateNormalsMap(horizontalNormals, VerticalNormals);
                GUI.enabled = true;
            }
            EditorGUILayout.EndVertical();
        }

        private void GenerateNormalsMap(Texture2D horizontals, Texture2D verticals)
        {
            // Create the asset if not existing yet
            if (OutputTexture == null)
            {
                OutputTexture = new Texture2D(Mathf.Max(horizontals.width, verticals.width), Mathf.Max(horizontals.height, verticals.height), TextureFormat.RGBA32, false);
            }

            // Actual map generation
            Color[] outputPixels = new Color[OutputTexture.width * OutputTexture.height];
            float uNormalized = 0.0f, vNormalized = 0.0f;
            Color ch, cv;
            float r, g, b;
            Vector2 xyNormal;
            Vector3 normal;
            for (int v = 0; v < OutputTexture.height; ++v)
            {
                vNormalized = (float)v / OutputTexture.height;
                for (int u = 0; u < OutputTexture.width; ++u)
                {
                    uNormalized = (float)u / OutputTexture.width;
                    ch = horizontals.GetPixelBilinear(uNormalized, vNormalized);
                    r = ch.r * 2.0f - 1.0f;
                    cv = verticals.GetPixelBilinear(uNormalized, vNormalized);
                    g = cv.g * 2.0f - 1.0f;
                    xyNormal = Vector2.ClampMagnitude(new Vector2(r, g), 0.999f);
                    r = xyNormal.x;
                    g = xyNormal.y;
                    b = (float)Math.Sqrt(1.0f - (double)(r * r) - (double)(g * g)); // z = sqrt (1 - x^2 - y^2)
                    normal = new Vector3(r, g, b).normalized;
                    outputPixels[u + v * OutputTexture.width] = new Color(normal.x * 0.5f + 0.5f, normal.y * 0.5f + 0.5f, normal.z * 0.5f + 0.5f, 1.0f);
                }
            }
            OutputTexture.SetPixels(outputPixels);
            OutputTexture.Apply();

            File.WriteAllBytes(Path.GetDirectoryName(Application.dataPath) + "/Assets/SpriteNormals.png", OutputTexture.EncodeToPNG());
            AssetDatabase.ImportAsset("Assets/SpriteNormals.png");
            OutputTexture = AssetDatabase.LoadMainAssetAtPath("Assets/SpriteNormals.png") as Texture2D;
        }
    }
}