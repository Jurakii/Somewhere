using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldColor: MonoBehaviour
{
    public Color Color1;
    public Color Color2;
    public Color Color3;
    public Color Color4;
    public Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = Color1;
        Shader.SetGlobalColor("_SpriteColorC", Color1);
        Shader.SetGlobalColor("_SpriteColorC2", Color2);
        Shader.SetGlobalColor("_SpriteColorC3", Color3);
        Shader.SetGlobalColor("_SpriteColorC4", Color4);
    }

}