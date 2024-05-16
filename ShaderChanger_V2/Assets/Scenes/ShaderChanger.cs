using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Collections;

public class ShaderChanger : OdinEditorWindow
{
    [MenuItem("Tools/SetShaderForMaterials")]

    private static void Init()
    {
        GetWindow<ShaderChanger>().Show();
    }


    void SetShaderForMaterials(string shaderName)
    {

        Shader choosenShader = Shader.Find(shaderName);

        List<Material> materials = new List<Material>();

        List<String> materialPaths = new List<String>();
        materialPaths = Selection.assetGUIDs
        .Select(assetguid => AssetDatabase.GUIDToAssetPath(assetguid))
        .Where(path => path.EndsWith(".mat", System.StringComparison.OrdinalIgnoreCase))
        .ToList();

        for (int i = 0; i < materialPaths.Count; i++)
        {
            materials.Add((Material)AssetDatabase.LoadAssetAtPath(materialPaths[i], typeof(Material)));
        }

        for (int i = 0; i < materials.Count; i++)
        {
            Texture main = materials[i].GetTexture("_MainTex");

            Texture normal = materials[i].GetTexture("_BumpMap");

            Texture metalic = materials[i].GetTexture("_SpecGlossMap");

            Texture emission = materials[i].GetTexture("_EmissionMap");

            Texture occlusion = materials[i].GetTexture("_OcclusionMap");

            materials[i].shader = choosenShader;

            materials[i].SetTexture("_BaseMap", main);

            materials[i].SetTexture("_BumpMap", normal);

            materials[i].SetTexture("_MetallicGlossMap", metalic);

            materials[i].SetTexture("_EmissionMap", emission);

            materials[i].SetTexture("_OcclusionMap", occlusion);

        }
    }

    [Button]
    void LitShader()
    {
        SetShaderForMaterials("Shader Graphs/TestShader");
    }
}
