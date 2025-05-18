using UnityEditor;

namespace PositionVisualizer
{
    [InitializeOnLoad]
    internal static class PositionVisualizer
    {
        static PositionVisualizer()
        {
            PositionVisualizerSettings.LoadSettings();
        }
    }
}
