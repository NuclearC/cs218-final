using UnityEngine;
using UnityEditor;
using System.IO;

public class ConvertToURPMaterials : EditorWindow
{
    [MenuItem("Tools/Convert All Materials to URP Lit")]
    static void ConvertAllMaterials()
    {
        string[] materialGUIDs = AssetDatabase.FindAssets("t:Material");
        int convertedCount = 0;

        foreach (string guid in materialGUIDs)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat.shader.name.Contains("Standard") || mat.shader.name.Contains("Legacy Shaders"))
            {
                mat.shader = Shader.Find("Universal Render Pipeline/Lit");

                // Optional: try auto-fix texture slots
                Texture albedo = mat.GetTexture("_MainTex");
                if (albedo != null)
                    mat.SetTexture("_BaseMap", albedo);

                Color color = mat.GetColor("_Color");
                mat.SetColor("_BaseColor", color);

                EditorUtility.SetDirty(mat);
                convertedCount++;
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"✅ Converted {convertedCount} materials to URP/Lit.");
    }
}
