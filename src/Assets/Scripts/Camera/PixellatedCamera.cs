using UnityEngine;
using UnityEngine.UI;

public class PixellatedCamera : MonoBehaviour
{
    public Camera MainCam;
    private Vector2 _resolution;
    public RenderTexture RT;
    public RawImage Image;
    public int PixelSize = 4;

    private void Awake()
    {
        _resolution = new Vector2(Screen.width, Screen.height);
        SetRenderTextureSize();
    }

    void Update()
    {
        if (_resolution.x != Screen.width || _resolution.y != Screen.height)
        {
            SetRenderTextureSize();
        }
    }

    void SetRenderTextureSize()
    {
        MainCam.targetTexture.Release();

        var newTex = new RenderTexture(RT);
        newTex.width = Screen.width / PixelSize;
        newTex.height = Screen.height / PixelSize;
        newTex.antiAliasing = 1;
        newTex.filterMode = FilterMode.Point;

        MainCam.targetTexture = newTex;
        Image.texture = newTex;
        RT = newTex;
    }
}
