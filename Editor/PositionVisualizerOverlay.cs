using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine.UIElements;

namespace PositionVisualizer
{
    [Overlay(typeof(SceneView), "Visualize Position")]
    class PositionVisualizerOverlay : Overlay
    {
        PositionVisualizerOverlay()
        {
            SceneView.duringSceneGui += Points.DrawInSceneView;
        }

        public override VisualElement CreatePanelContent()
        {
            const int fieldWidth = 50;

            var root = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                },
            };

            var xField = new FloatField
            {
                style = { width = fieldWidth },
            };

            var yField = new FloatField
            {
                style = { width = fieldWidth },
            };

            var zField = new FloatField
            {
                style = { width = fieldWidth },
            };

            var addButton = new Button(() =>
            {
                Points.Add(xField.value, yField.value, zField.value);
            }) { text = "Add" };

            var clearButton = new Button(Points.Clear) { text = "Clear" };

            root.Add(xField);
            root.Add(yField);
            root.Add(zField);
            root.Add(addButton);
            root.Add(clearButton);
            return root;
        }
    }
}
