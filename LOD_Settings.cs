using UnityEditor;
using UnityEngine;

public class LOD_Settings : EditorWindow
{
    static private float culled_distance;
    static private float lod1_distance;

    [MenuItem("X-Ray Plugin/LOD System/Open LOD Settings")]
    public static void ShowWindow()
    {
        GetWindow<LOD_Settings>(false, "LOD Settings", true);
        culled_distance = 0.06f;
        lod1_distance = 0.4f;
    }

    void OnGUI()
    {

        EditorGUILayout.LabelField("Settings");
        EditorGUILayout.BeginVertical("box");
        culled_distance = EditorGUILayout.Slider("Culled distance", culled_distance, 0f, 1f);
        lod1_distance = EditorGUILayout.Slider("Lod1 Distance", lod1_distance, 0f, 1f);
        EditorGUILayout.EndVertical();
        EditorGUILayout.LabelField("Controls");
        if (GUILayout.Button("Start"))
        {
            SettingLOD.culled_distance = culled_distance;
            SettingLOD.lod1_distance = lod1_distance;
            CustomUtilites.LOD_Generator();
        }
        EditorGUILayout.LabelField("Create Groupe");
        if (GUILayout.Button("Split LODs"))
        {
            CustomUtilites.GetSelectedUVs();
        }
    }
}

public static class SettingLOD
{
    public static float culled_distance;
    public static float lod1_distance;
}
