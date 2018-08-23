using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class AssetPreviewRender : MonoBehaviour {


    [MenuItem("Assets/Clear Cache")]
    public static void ClearCache()
    {
        Caching.ClearCache();
    }


    [MenuItem("Assets/Render Asset Preview")]
    public static void RenderAssetPreview()
    {
        Texture2D assetPreviewTexture = AssetPreview.GetAssetPreview(Selection.activeObject);

        Color[] pixels = assetPreviewTexture.GetPixels();

        for(int i=0;i<pixels.Length;i++)
        {
            pixels[i].a = 0;
        }

        Debug.Log(assetPreviewTexture.GetPixel(0, 0));

        Texture2D saveTexture = new Texture2D(assetPreviewTexture.width, assetPreviewTexture.height, TextureFormat.ARGB32, false);
        saveTexture.SetPixels(0, 0, assetPreviewTexture.width, assetPreviewTexture.height, pixels);

        saveTexture.Apply();
        byte[] textureBytes = saveTexture.EncodeToPNG();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        string imagePath = path.Remove(path.LastIndexOf('.')) + "Preview.png";

        File.WriteAllBytes(imagePath, textureBytes);
        AssetDatabase.Refresh();
    }

}
