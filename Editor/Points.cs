using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PositionVisualizer
{
    public static class Points
    {
        const string editorPrefsKey = "PositionVisualizer.points";

        static readonly List<(Vector3, Color)> points = new();

        static Points()
        {
            PositionVisualizerSettings.LoadSettings();
            Load();
        }

        public static void Add(float x, float y, float z)
        {
            var position = new Vector3(x, y, z);
            Add(position);
        }

        public static void Add(params Vector3[] positions)
        {
            foreach (var position in positions)
            {
                var color = PositionVisualizerSettings.randomizePointColor
                    ? new Color(Random.value, Random.value, Random.value)
                    : PositionVisualizerSettings.pointColor;

                points.Add((position, color));
            }

            Save();
            Frame(positions);
        }

        public static void Clear()
        {
            points.Clear();
            Save();
        }

        internal static void DrawInSceneView(SceneView sceneView)
        {
            foreach (var (position, color) in points)
            {
                Handles.color = color;
                var size = PositionVisualizerSettings.pointSize;

                if (!PositionVisualizerSettings.draw3DPoint)
                {
                    size *= HandleUtility.GetHandleSize(position);
                }

                switch (PositionVisualizerSettings.pointShape)
                {
                    case PositionVisualizerSettings.Shape.Cube:
                        Handles.CubeHandleCap(0, position, Quaternion.identity, size, EventType.Repaint);
                        break;

                    case PositionVisualizerSettings.Shape.Sphere:
                        Handles.SphereHandleCap(0, position, Quaternion.identity, size, EventType.Repaint);
                        break;
                }

                if (PositionVisualizerSettings.showLabel)
                {
                    Handles.Label(position, $"({position.x:0.##}, {position.y:0.##}, {position.z:0.##})");
                }
            }
        }

        static void Frame(Vector3[] positions)
        {
            if (positions == null || positions.Length == 0)
            {
                return;
            }

            var bounds = new Bounds(positions[0], Vector3.one);

            for (var i = 1; i < positions.Length; i++)
            {
                bounds.Encapsulate(positions[i]);
            }

            SceneView.lastActiveSceneView.Frame(bounds, false);
        }

        static void Load()
        {
            var serialized = EditorPrefs.GetString(editorPrefsKey);
            points.Clear();
            points.AddRange(PointsSerializer.Deserialize(serialized));
        }

        static void Save()
        {
            var serialized = PointsSerializer.Serialize(points);
            EditorPrefs.SetString(editorPrefsKey, serialized);
        }
    }
}
