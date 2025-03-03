using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PositionVisualizer
{
    static class PointsSerializer
    {
        internal static IEnumerable<(Vector3, Color)> Deserialize(string serialized)
        {
            return serialized
                .Split('\n')
                .Where(static line => !string.IsNullOrEmpty(line))
                .Select(static line =>
                {
                    var values = line.Split(',');

                    var position = new Vector3(
                        float.Parse(values[0]),
                        float.Parse(values[1]),
                        float.Parse(values[2])
                    );

                    var color = new Color(
                        float.Parse(values[3]),
                        float.Parse(values[4]),
                        float.Parse(values[5]),
                        float.Parse(values[6])
                    );

                    return (position, color);
                });
        }

        internal static string Serialize(IEnumerable<(Vector3, Color)> points)
        {
            var lines = points.Select(static point =>
            {
                var (position, color) = point;

                return string.Join(
                    ',',
                    position.x,
                    position.y,
                    position.z,
                    color.r,
                    color.g,
                    color.b,
                    color.a
                );
            });

            return string.Join('\n', lines);
        }
    }
}
