using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public RenderTexture _rt;

    public Camera cam;

    //cube相关
    public Transform[] cubeTransform;
    public Mesh cubeMesh;
    public Material pureColorMaterial;
    private RenderTexture depthTexture;


    public SkyboxDraw skyboxDraw;


    void Start()
    {
        _rt = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
        //深度
        depthTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.Depth, RenderTextureReadWrite.Linear);
    }

    // Update is called once per frame
    void Update()
    {
        //OnPostRender();
    }

    private static void ReSize(RenderTexture rt, int width, int height)
    {
        rt.Release();
        rt.width = width;
        rt.height = height;
        rt.Create();
    }
    private void OnPostRender()//相机完成渲染后调用
    {
        ReSize(_rt, (int)(cam.pixelHeight ), (int)(cam.pixelWidth ));
        ReSize(depthTexture, (int)(cam.pixelHeight), (int)(cam.pixelWidth));
        //Shader.SetGlobalTexture(_DepthTexture, depthTexture);

        Graphics.SetRenderTarget(_rt);
        GL.Clear(true, true, Color.grey);

        pureColorMaterial.color = new Color(0, 1.0f, 0.0f);

        //1.setPass
        pureColorMaterial.SetPass(0);

        //2.Draw
        foreach(var i in cubeTransform)
        {
            Graphics.DrawMeshNow(cubeMesh, i.localToWorldMatrix);
        }

        skyboxDraw.DrawSkybox(cam, _rt.colorBuffer, depthTexture.depthBuffer);

        Graphics.Blit(_rt, cam.targetTexture);
    }


}
