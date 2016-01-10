using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;

[CustomEditor(typeof(Decal))]
public class DecalEditor : Editor
{

    private List<Material> materials;

    private Matrix4x4 oldMatrix;
    private Vector3 oldScale;
    private static bool showAffectedObject = false;
    private GameObject[] affectedObjects;


    void OnEnable()
    {
        Decal[] decals = (Decal[])GameObject.FindObjectsOfType(typeof(Decal));
        materials = new List<Material>();
        foreach (Decal decal in decals)
        {
            if (decal.material != null && !materials.Contains(decal.material))
            {
                materials.Add(decal.material);
            }
        }
    }

    public override void OnInspectorGUI()
    {
        Decal decal = (Decal)target;

        decal.material = DrawMaterialList(decal.material, materials);
        decal.material = AssetField<Material>("Material", decal.material);
        if (decal.material != null && !materials.Contains(decal.material))
        {
            materials.Add(decal.material);
        }
        EditorGUILayout.Separator();

        if (decal.material != null && decal.material.mainTexture != null)
        {
            decal.sprite = DrawSpriteList(decal.sprite, decal.material.mainTexture);
            if (decal.sprite && decal.sprite.texture != decal.material.mainTexture) decal.sprite = null;
        }
        EditorGUILayout.Separator();

        decal.maxAngle = EditorGUILayout.FloatField("Max Angle", decal.maxAngle);
        decal.maxAngle = Mathf.Clamp(decal.maxAngle, 1, 180);

        decal.pushDistance = EditorGUILayout.FloatField("Push Distance", decal.pushDistance);
        decal.pushDistance = Mathf.Clamp(decal.pushDistance, 0.01f, 1);
        EditorGUILayout.Separator();

        decal.affectedLayers = LayerMaskField("Affected Layers", decal.affectedLayers);

        showAffectedObject = EditorGUILayout.Foldout(showAffectedObject, "Affected Objects");
        if (showAffectedObject && affectedObjects != null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(15);
            GUILayout.BeginVertical();
            foreach (GameObject go in affectedObjects)
            {
                EditorGUILayout.ObjectField(go, typeof(GameObject), true);
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.Separator();

        GUILayout.Box("Left Ctrl + Left Mouse Button - Set position and normal of decal", GUILayout.ExpandWidth(true));

        if (GUI.changed)
        {
            BuildDecal(decal);
        }
    }

    private static T AssetField<T>(string label, T obj) where T : Object
    {
        return (T)EditorGUILayout.ObjectField(label, (T)obj, typeof(T), false);
    }

    private static Material DrawMaterialList(Material material, List<Material> list)
    {
        string[] names = new string[list.Count];
        for (int i = 0; i < list.Count; i++)
        {
            names[i] = list[i].name;
        }

        int selected = list.IndexOf(material);
        selected = EditorGUILayout.Popup("Material", selected, names);
        if (selected != -1) return list[selected];
        return null;
    }

    private static Sprite DrawSpriteList(Sprite sprite, Texture texture)
    {
        string path = AssetDatabase.GetAssetPath(texture);
        Object[] objs = AssetDatabase.LoadAllAssetsAtPath(path);
        List<Sprite> sprites = new List<Sprite>();
        foreach (Object o in objs)
        {
            if (o is Sprite) sprites.Add((Sprite)o);
        }

        return DrawSpriteList(sprite, sprites.ToArray(), texture);
    }

    private static Sprite DrawSpriteList(Sprite sprite, Sprite[] list, Texture texture)
    {
        GUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(50));
        for (int i = 0, y = 0; i < list.Length; y++)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < 5; x++, i++)
            {
                Rect rect = GUILayoutUtility.GetAspectRect(1);
                if (i < list.Length)
                {
                    Sprite spr = list[i];
                    bool selected = DrawItem(rect, spr, sprite == spr);
                    if (selected) sprite = spr;
                }
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
        return sprite;
    }

    private static bool DrawItem(Rect rect, Sprite sprite, bool selected)
    {
        if (selected)
        {
            GUI.color = Color.blue;
            GUI.DrawTexture(rect, EditorGUIUtility.whiteTexture);
            GUI.color = Color.white;
        }

        Texture texture = sprite.texture;
        Rect texRect = sprite.rect;
        texRect.x /= texture.width;
        texRect.y /= texture.height;
        texRect.width /= texture.width;
        texRect.height /= texture.height;
        GUI.DrawTextureWithTexCoords(rect, texture, texRect);

        selected = Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition);
        if (selected)
        {
            GUI.changed = true;
            Event.current.Use();
            return true;
        }
        return false;
    }


    void OnSceneGUI()
    {
        Decal decal = (Decal)target;

        if (Event.current.control)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        }

        if (Event.current.control && Event.current.type == EventType.MouseDown && Event.current.button == 0)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, 50))
            {
                float tmp = decal.transform.localEulerAngles.z;
                decal.transform.position = hit.point; decal.transform.forward = -hit.normal;
                Vector3 t = decal.transform.localEulerAngles;
                t.z = tmp;
                decal.transform.localEulerAngles = t;
            }
        }

        Vector3 scale = decal.transform.localScale;
        if (decal.sprite != null)
        {
            float ratio = (float)decal.sprite.rect.width / decal.sprite.rect.height;
            if (oldScale.x != scale.x)
            {
                scale.y = scale.x / ratio;
            }
            else
            if (oldScale.y != scale.y)
            {
                scale.x = scale.y * ratio;
            }
            else
            if (scale.x != scale.y * ratio)
            {
                scale.x = scale.y * ratio;
            }
            decal.transform.localScale = scale;
        }

        bool hasChanged = oldMatrix != decal.transform.localToWorldMatrix;
        oldMatrix = decal.transform.localToWorldMatrix;
        oldScale = decal.transform.localScale;


        if (hasChanged)
        {
            BuildDecal(decal);
        }
    }

    private static LayerMask LayerMaskField(string label, LayerMask mask)
    {
        List<string> layers = new List<string>();
        for (int i = 0; i < 32; i++)
        {
            string name = LayerMask.LayerToName(i);
            if (name != "") layers.Add(name);
        }
        return EditorGUILayout.MaskField(label, mask, layers.ToArray());
    }

    private static bool IsLayerContains(LayerMask mask, int layer)
    {
        return (mask.value & 1 << layer) != 0;
    }


    private void BuildDecal(Decal decal)
    {
        MeshFilter filter = decal.GetComponent<MeshFilter>();
        if (filter == null) filter = decal.gameObject.AddComponent<MeshFilter>();
        if (decal.GetComponent<Renderer>() == null) decal.gameObject.AddComponent<MeshRenderer>();
        decal.GetComponent<Renderer>().material = decal.material;

        if (decal.material == null || decal.sprite == null)
        {
            filter.mesh = null;
            return;
        }

        affectedObjects = GetAffectedObjects(decal.GetBounds(), decal.affectedLayers);
        foreach (GameObject go in affectedObjects)
        {
            DecalBuilder.BuildDecalForObject(decal, go);
        }
        DecalBuilder.Push(decal.pushDistance);

        Mesh mesh = DecalBuilder.CreateMesh();
        if (mesh != null)
        {
            mesh.name = "DecalMesh";
            filter.mesh = mesh;
        }
    }

    private static GameObject[] GetAffectedObjects(Bounds bounds, LayerMask affectedLayers)
    {
        MeshRenderer[] renderers = (MeshRenderer[])GameObject.FindObjectsOfType<MeshRenderer>();
        List<GameObject> objects = new List<GameObject>();
        foreach (Renderer r in renderers)
        {
            if (!r.enabled) continue;
            if (!IsLayerContains(affectedLayers, r.gameObject.layer)) continue;
            if (r.GetComponent<Decal>() != null) continue;

            if (bounds.Intersects(r.bounds))
            {
                objects.Add(r.gameObject);
            }
        }
        return objects.ToArray();
    }



}