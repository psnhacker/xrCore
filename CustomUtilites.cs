using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class CustomUtilites : Editor
{
    [MenuItem("X-Ray Plugin/LOD System/Generate Selected LOD")]
    public static void LOD_Generator()
    {
        //        0-1 == 0 - 100%
        //float culled_distance = 0.06f; 
        //float lod1_distance = 0.4f;
        float culled_distance = SettingLOD.culled_distance;
        float lod1_distance = SettingLOD.lod1_distance;
        GameObject[] objectArray = Selection.gameObjects;
        for (int i = 0; i < objectArray.Length; i++)
        {
            LOD[] lods = new LOD[2];
            Renderer[] rend = new Renderer[1];
            rend[0] = objectArray[i].GetComponent<MeshRenderer>();
            lods[1] = new LOD(culled_distance, rend);
            int childrenCount = objectArray[i].transform.childCount;
            Renderer[] rend2 = new Renderer[childrenCount];
            for (int j = 0; j < childrenCount; j++)
            {
                rend2[j] = objectArray[i].transform.GetChild(j).GetComponent<MeshRenderer>();
            }
            lods[0] = new LOD(lod1_distance, rend2);
            LODGroup lg = objectArray[i].AddComponent<LODGroup>();
            lg.SetLODs(lods);
            lg.fadeMode = LODFadeMode.CrossFade;
            lg.animateCrossFading = true;
        }
    }
    
    
        [MenuItem("X-Ray Plugin/LOD System/Selected Mesh UVs")]
    public static void GetSelectedUVs()
    {
        GameObject[] objectArray = Selection.gameObjects;
        List<Vector2[]> vector2s = new List<Vector2[]>();
        for (int i = 0; i < objectArray.Length; i++)
        {
            Vector2[] currentUVs = objectArray[i].transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh.uv;
            if (i == 0)
            {
                vector2s.Add(currentUVs);
                continue;
            }
            bool value = false;
            for (int j = 0; j < vector2s.Count; j++)
            {
                if (currentUVs[0] == vector2s[j][0] && currentUVs[1] == vector2s[j][1])
                {
                    value = true;
                    break;
                }
            }
            if (!value) vector2s.Add(currentUVs);
        }
        Debug.Log(vector2s.Count);
        GameObject[] lodGroupes = new GameObject[vector2s.Count];
        GameObject allLod = new GameObject("LOD_Groupes");
        for (int k = 0; k < lodGroupes.Length; k++)
        {
            GameObject lod = new GameObject("LOD_" + k);
            lod.transform.SetParent(allLod.transform);
            lodGroupes[k] = lod;
        }
        for (int i = 0; i < objectArray.Length; i++)
        {
            Vector2[] currentUVs = objectArray[i].transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh.uv;
            for (int j = 0; j < vector2s.Count; j++)
            {
                if (currentUVs[0] == vector2s[j][0] && currentUVs[1] == vector2s[j][1]) objectArray[i].transform.SetParent(lodGroupes[j].transform);
            }
        }
    }
    
    public static void SetCollision()
    {
        string[] generate =
        {
            "bark",
            "veh",
            "shiffer"
        };
        GameObject[] objectArray = Selection.gameObjects;
        for (int i = 0; i < objectArray.Length; i++)
        {
            MeshCollider collider = objectArray[i].GetComponent<MeshCollider>();
            if (collider != null) DestroyImmediate(collider);
            int childrenCount = objectArray[i].transform.childCount;
            for (int j = 0; j < childrenCount; j++)
            {
                string matName = objectArray[i].transform.GetChild(j).GetComponent<MeshRenderer>().sharedMaterial.name;
                for (int k = 0; k < generate.Length; k++)
                {
                    if (matName.Contains(generate[k]))
                    {
                        objectArray[i].transform.GetChild(j).gameObject.AddComponent<MeshCollider>();
                    }
                }
            }

        }
    }
}
