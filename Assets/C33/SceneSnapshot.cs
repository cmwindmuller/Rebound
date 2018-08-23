using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class SceneSnapshot : MonoBehaviour {

    [Header("Camera Settins")]
    public  Camera mainCamera;
    public bool transparent = true;
    public float cameraZ = -1;

    [Header("Scene")]
    public GameObject foreground;
    public GameObject background;
    public float foreZ, backZ;
    public float foreScale=1, backScale=1;

    [Header("Prefabs")]
    public int current;
    private int last;
    private GameObject currentObject;
    public GameObject[] assets;

    private void localZSet(GameObject g,float z)
    {
        Vector3 tempPosition = g.transform.localPosition;
        tempPosition.z = z;
        g.transform.localPosition = tempPosition;
    }

    public void OnValidate()
    {
        if (mainCamera == null)
            return;

        if (transparent && mainCamera.clearFlags != CameraClearFlags.Depth)
            mainCamera.clearFlags = CameraClearFlags.Depth;

        localZSet(mainCamera.gameObject, cameraZ);
        localZSet(foreground, foreZ);
        localZSet(background, backZ);

        if (current < 0 || assets.Length < 2) current = 0;
        else if (current > assets.Length - 1) current = assets.Length - 1;



        /*mainCamera = GetComponentInChildren<Camera>();
        mainCamera.transform.localPosition = new Vector3(0, 0, distance);
        Filter[] filters = { filterFront, filterBack };
        foreach (Filter f in filters)
        {
            f.quad.transform.localPosition = Vector3.forward * f.distance;
            f.quad.transform.localScale = Vector3.one * Mathf.Abs(f.distance - distance) * f.scale;
        }*/
    }

    public void Snapshot()
    {
        string filePath = Application.dataPath + "/Snapshots/";
        System.IO.FileInfo file = new System.IO.FileInfo( filePath );
        file.Directory.Create();

        for (int i=0;i<assets.Length;i++)
        {
            GameObject obj = PrefabUtility.InstantiatePrefab( assets[ i ] ) as GameObject;
            try
            {
                TakeSnapshot(filePath + assets[i].name);
            }
            finally
            {
                DestroyImmediate(obj);
            }
        }

        AssetDatabase.Refresh();
    }

    private void TakeSnapshot(string s)
    {
        RenderTexture rt = new RenderTexture(mainCamera.pixelWidth, mainCamera.pixelHeight, 32);
        mainCamera.targetTexture = rt;
        Texture2D screenshot = new Texture2D(mainCamera.pixelWidth, mainCamera.pixelHeight, TextureFormat.ARGB32, false);
        mainCamera.Render();
        RenderTexture.active = rt;
        screenshot.ReadPixels(new Rect(0, 0, mainCamera.pixelWidth, mainCamera.pixelHeight), 0, 0);
        screenshot.Apply();
        mainCamera.targetTexture = null;
        RenderTexture.active = null;
        File.WriteAllBytes( s + ".png", screenshot.EncodeToPNG());
        Debug.Log(s + "saved");
    }

}
