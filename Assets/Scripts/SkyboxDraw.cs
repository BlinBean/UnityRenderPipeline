using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxDraw : MonoBehaviour
{
    private static Mesh m_mesh ;
    private static Vector4[] corners = new Vector4[4];
    private static int _Corner = Shader.PropertyToID("_Corner");
    public Material skyboxMaterial;

    public void DrawSkybox(Camera cam)
    {
        //获得Far4个视口坐标
        corners[0] = cam.ViewportToScreenPoint(new Vector3(0, 0, cam.farClipPlane));
        corners[1] = cam.ViewportToScreenPoint(new Vector3(1, 0, cam.farClipPlane));
        corners[2] = cam.ViewportToScreenPoint(new Vector3(0, 1, cam.farClipPlane));
        corners[3] = cam.ViewportToScreenPoint(new Vector3(1, 1, cam.farClipPlane));

        
        skyboxMaterial.SetVectorArray(_Corner, corners);
        skyboxMaterial.SetPass(0);

        Graphics.DrawMeshNow(fullScreenMesh, Matrix4x4.identity);
    }

    //设置Mesh
    public static Mesh fullScreenMesh
    {
        get
        {
            if (m_mesh != null)
                return m_mesh;
            m_mesh = new Mesh();
            //NDC,注意顺序
            m_mesh.vertices = new Vector3[] {
                new Vector4(-1,-1,0, 1),
                new Vector4(-1,1,0, 1),
                new Vector4(1,1,0, 1),
                new Vector4(1,-1,0, 1)
            };
            //DX
            m_mesh.uv = new Vector2[] {
                new Vector2(0, 1),
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(1, 1),

            };
            //设置mesh顶点索引
            m_mesh.SetIndices(new int[] {0, 1, 2, 3 },
                                MeshTopology.Quads,
                              0);
            return m_mesh;
        }
    }

}
