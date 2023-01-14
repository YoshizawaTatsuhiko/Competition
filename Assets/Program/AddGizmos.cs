using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ql¿
// https://github.com/code-beans/GizmoExtensions/blob/master/src/GizmosExtensions.cs

namespace MyGizmos
{
    /// <summary>Vµ¢GizmosðÇÁ·é</summary>
    public static class AddGizmos
    {
        public static void DrawWireCone(Vector3 coneTip, Vector3 direction, float height, float circleRadius, float segments = 20f)
        {
            Vector3 dest = coneTip + direction.normalized * height;
            Gizmos.DrawLine(coneTip, dest);

            //~Ì~ð`«nßén_
            Vector3 from = dest;
            from.y = circleRadius;
            Gizmos.DrawLine(dest, from);

            float degree = Mathf.PI * 2 * Mathf.Rad2Deg;

            //ÁÊðßé
            int addition = Mathf.RoundToInt(degree / segments);
        }

        public static void DrawCircle()
        {

        }
    }
}
