using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public RenderTexture _rt;
    public Camera cam;

    //cubeœ‡πÿ
    public Transform[] cubeTransform;
    public Mesh cubeMesh;
    public Material pureColorMaterial;

    public SkyboxDraw skyboxDraw;


    void Start()
    {
        _rt = new RenderTexture(Screen.width, Screen.height, 24);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static void ReSize(RenderTexture rt, int width, int height)
    {
        rt.Release();
        rt.width = width;
        rt.height = height;
        rt.Create();
    }
    private void OnPostRender()
    {
        ReSize(_rt, (int)(cam.pixelHeight ), (int)(cam.pixelWidth ));

        //Camera cam = Camera.current;
        Graphics.SetRenderTarget(_rt);
        GL.Clear(true, true, Color.red);

        pureColorMaterial.color = new Color(0, 0.5f, 0.8f);

        //1.setPass
        pureColorMaterial.SetPass(0);

        //2.Draw
        foreach(var i in cubeTransform)
        {
            //Graphics.DrawMeshNow(cubeMesh, i.localToWorldMatrix);
        }

        skyboxDraw.DrawSkybox(cam);

        Graphics.Blit(_rt, cam.targetTexture);
    }


}
