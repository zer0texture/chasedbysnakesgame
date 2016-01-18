using UnityEngine;
using System.Collections;

public class PostProcessFilter : MonoBehaviour
{

    public Material playMat;
    public Material cellMat;
    public RenderTexture mainRenderTexture;
    public RenderTexture cellRenderTexture;

    public Camera PlayerCamera;
    public Camera CellshadeCamera;

    // Use this for initialization
    void Start()
    {

        mainRenderTexture = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32);
        mainRenderTexture.Create();
        cellRenderTexture = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32);
        cellRenderTexture.Create();

        PlayerCamera.depthTextureMode = DepthTextureMode.Depth;
        CellshadeCamera.depthTextureMode = DepthTextureMode.Depth;
        PlayerCamera.targetTexture = mainRenderTexture;
        CellshadeCamera.targetTexture = cellRenderTexture;

        CellshadeCamera.SetTargetBuffers(CellshadeCamera.targetTexture.colorBuffer, PlayerCamera.targetTexture.depthBuffer);

    }

    void OnPostRender()
    {
        
        PlayerCamera.cullingMask = 1 << LayerMask.NameToLayer("Nothing");
        PlayerCamera.clearFlags = CameraClearFlags.SolidColor;
        PlayerCamera.Render();
        
        CellshadeCamera.Render();
        PlayerCamera.cullingMask = 1 << LayerMask.NameToLayer("Non-Whitelit");
        PlayerCamera.clearFlags = CameraClearFlags.Nothing;
        PlayerCamera.Render();

        Graphics.Blit(cellRenderTexture, cellMat);
        Graphics.Blit(mainRenderTexture, playMat);

    }
}
