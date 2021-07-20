using UnityEngine;

namespace Spectra.Extensions
{
    public class GizmoExtension
    {
        public enum DoubleAxis
        {
            XY,
            XZ,
            YZ,
        }

        static Vector3 GetAxisScale(DoubleAxis axis) => axis switch
        {
            DoubleAxis.XY => new Vector3(1, 1),
            DoubleAxis.XZ => new Vector3(1, 0, 1),
            DoubleAxis.YZ => new Vector3(0, 1, 1),
            _ => new Vector3(0, 0),
        };

        public static void DrawArrow(Vector3 start, Vector3 direction, float arrowLength, float headLength = .5f, float headAngle = 20f)
        {
            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + headAngle, 0) * Vector3.forward;
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - headAngle, 0) * Vector3.forward;

            var fullLength = direction * arrowLength;
            Gizmos.DrawRay(start, fullLength);
            Gizmos.DrawRay(start + fullLength, right * headLength);
            Gizmos.DrawRay(start + fullLength, left * headLength);
        }

        public static void DrawSquare(Vector3 center, float size, DoubleAxis axis)
        {
            var oldMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(center, Quaternion.identity, GetAxisScale(axis));
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(size, size, size));
            Gizmos.matrix = oldMatrix;
        }

        public static void DrawCircle(Vector3 center, float radius, DoubleAxis axis)
        {
            var oldMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(center, Quaternion.identity, GetAxisScale(axis));
            Gizmos.DrawWireSphere(Vector3.zero, radius);
            Gizmos.matrix = oldMatrix;
        }
    }

}