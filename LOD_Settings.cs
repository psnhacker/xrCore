using UnityEditor;
using UnityEngine;

public class LOD_Settings : EditorWindow
{
    static private float culled_distance;
    static private float lod1_distance;
    private enum Settings
    {
        None,
        Trees,
        Bush,
        Vehicles
    }
    static Settings settings = Settings.None;

    [MenuItem("X-Ray Plugin/LOD System/Open LOD Settings")]
    public static void ShowWindow()
    {
        GetWindow<LOD_Settings>(false, "LOD Settings", true);
        DefoultSettings();
    }

    void OnGUI()
    {

        EditorGUILayout.LabelField("Settings");
        EditorGUILayout.BeginVertical("box");
        culled_distance = EditorGUILayout.Slider("Culled distance", culled_distance, 0f, 1f);
        lod1_distance = EditorGUILayout.Slider("Lod1 Distance", lod1_distance, 0f, 1f);
        settings = (Settings)EditorGUILayout.EnumPopup(settings);
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if(settings != Settings.None)
        {
            switch (settings)
            {
                case Settings.Trees:
                    culled_distance = 0.03f;
                    lod1_distance = 0.35f;
                    break;
                case Settings.Bush:
                    culled_distance = 0.03f;
                    lod1_distance = 0.21f;
                    break;
                case Settings.Vehicles:
                    culled_distance = 0.08f;
                    lod1_distance = 0.21f;
                    break;
                default:
                    break;
            }
        }
        if (GUILayout.Button("Reset", GUILayout.Width(100), GUILayout.Height(18)))
        {
            DefoultSettings();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        EditorGUILayout.LabelField("Controls");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Start", GUILayout.Width(130), GUILayout.Height(25)))
        {
            StartGeneration();
        }
        if (GUILayout.Button("Split LODs", GUILayout.Width(130), GUILayout.Height(25)))
        {
            CustomUtilites.GetSelectedUVs();
        }
        EditorGUILayout.EndHorizontal();
    }

    private static void StartGeneration()
    {
        SettingLOD.culled_distance = culled_distance;
        SettingLOD.lod1_distance = lod1_distance;
        CustomUtilites.LOD_Generator();
    }

    private static void DefoultSettings()
    {
        culled_distance = 0.06f;
        lod1_distance = 0.4f;
        settings = Settings.None;
    }
}

public static class SettingLOD
{
    public static float culled_distance;
    public static float lod1_distance;
}
