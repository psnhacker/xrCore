using UnityEditor;
using UnityEngine;

public class LOD_Settings : EditorWindow
{
    [MenuItem("X-Ray Plugin/LOD System/Open LOD Settings")]
    public static void ShowWindow()
    {
        GetWindow<LOD_Settings>(false, "LOD Settings", true);
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Settings");
        float culled_distance = EditorGUILayout.Slider("Culled distance", 0.06f, 0f, 1f);
        float lod1_distance = EditorGUILayout.Slider("Lod1 Distance", 0.4f, 0f, 1f);
        EditorGUILayout.LabelField("Controls");
        if (GUILayout.Button("Set Value"))
        {
            SettingLOD.culled_distance = culled_distance;
            SettingLOD.lod1_distance = lod1_distance;
            Debug.Log("Pressed button Set Value");
        }
        if (GUILayout.Button("Start"))
        {
            CustomUtilites.LOD_Generator();
            Debug.Log("Generation lod");
        }
        EditorGUILayout.LabelField("Create Groupe");
        if (GUILayout.Button("Split LODs"))
        {
            CustomUtilites.GetSelectedUVs();
            Debug.Log("Pressed Split lod");
        }
    }
}

public static class SettingLOD
{
    public static float culled_distance;
    public static float lod1_distance;

}
