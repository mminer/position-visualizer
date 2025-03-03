using UnityEditor;
using UnityEngine;

namespace PositionVisualizer
{
    static class PositionVisualizerSettings
    {
        internal enum Shape
        {
            Cube,
            Sphere,
        }

        const string editorPrefsKey_draw3DPoint = "PositionVisualizer.draw3DPoint";
        const string editorPrefsKey_pointColorR = "PositionVisualizer.pointColorR";
        const string editorPrefsKey_pointColorG = "PositionVisualizer.pointColorG";
        const string editorPrefsKey_pointColorB = "PositionVisualizer.pointColorB";
        const string editorPrefsKey_pointColorA = "PositionVisualizer.pointColorA";
        const string editorPrefsKey_pointShape = "PositionVisualizer.pointShape";
        const string editorPrefsKey_pointSize = "PositionVisualizer.pointSize";
        const string editorPrefsKey_randomizePointColor = "PositionVisualizer.randomizePointColor";
        const string editorPrefsKey_showLabel = "PositionVisualizer.showLabel";

        internal static bool draw3DPoint { get; private set; }
        internal static Color pointColor { get; private set; }
        internal static Shape pointShape { get; private set; }
        internal static float pointSize { get; private set; }
        internal static bool randomizePointColor { get; private set; }
        internal static bool showLabel { get; private set; }

        [SettingsProvider]
        static SettingsProvider CreateSettingsProvider()
        {
            LoadSettings();

            return new SettingsProvider("Preferences/Position Visualizer", SettingsScope.User)
            {
                guiHandler = static _ =>
                {
                    draw3DPoint = EditorGUILayout.Toggle("Draw 3D Point", draw3DPoint);
                    pointColor = EditorGUILayout.ColorField("Point Color", pointColor);

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.PrefixLabel(" ");
                        randomizePointColor = EditorGUILayout.ToggleLeft("Randomize", randomizePointColor);
                    }
                    EditorGUILayout.EndHorizontal();

                    pointShape = (Shape)EditorGUILayout.EnumPopup("Point Shape", pointShape);
                    pointSize = EditorGUILayout.FloatField("Point Size", pointSize);
                    showLabel = EditorGUILayout.Toggle("Show Label", showLabel);

                    if (GUI.changed)
                    {
                        SaveSettings();
                    }
                },
                keywords = new[]
                {
                    "Color",
                    "Point",
                    "Position",
                    "Size",
                    "Visualizer",
                },
                label = "Position Visualizer",
            };
        }

        internal static void LoadSettings()
        {
            draw3DPoint = EditorPrefs.GetBool(editorPrefsKey_draw3DPoint);

            pointColor = new Color(
                EditorPrefs.GetFloat(editorPrefsKey_pointColorR, 1f),
                EditorPrefs.GetFloat(editorPrefsKey_pointColorG, 0f),
                EditorPrefs.GetFloat(editorPrefsKey_pointColorB, 0f),
                EditorPrefs.GetFloat(editorPrefsKey_pointColorA, 1f)
            );

            pointShape = (Shape)EditorPrefs.GetInt(editorPrefsKey_pointShape);
            pointSize = EditorPrefs.GetFloat(editorPrefsKey_pointSize, 0.1f);
            randomizePointColor = EditorPrefs.GetBool(editorPrefsKey_randomizePointColor);
            showLabel = EditorPrefs.GetBool(editorPrefsKey_showLabel, true);
        }

        static void SaveSettings()
        {
            EditorPrefs.SetBool(editorPrefsKey_draw3DPoint, draw3DPoint);
            EditorPrefs.SetFloat(editorPrefsKey_pointColorR, pointColor.r);
            EditorPrefs.SetFloat(editorPrefsKey_pointColorG, pointColor.g);
            EditorPrefs.SetFloat(editorPrefsKey_pointColorB, pointColor.b);
            EditorPrefs.SetFloat(editorPrefsKey_pointColorA, pointColor.a);
            EditorPrefs.SetInt(editorPrefsKey_pointShape, (int)pointShape);
            EditorPrefs.SetFloat(editorPrefsKey_pointSize, pointSize);
            EditorPrefs.SetBool(editorPrefsKey_randomizePointColor, randomizePointColor);
            EditorPrefs.SetBool(editorPrefsKey_showLabel, showLabel);
        }
    }
}
