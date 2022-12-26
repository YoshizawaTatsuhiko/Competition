using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�Q�l����
//https://github.com/code-beans/GizmoExtensions/blob/master/src/GizmosExtensions.cs

namespace MyGizmos
{
    /// <summary>�V����Gizmos��ǉ�����</summary>
    public static class AddGizmos
    {
        public static void DrawWireCone(Vector3 coneTip,
            Vector3 direction, float radius, Quaternion rotation, float segments = 20f)
        {
            //Gizmos�̍s���ۑ����Ă���
            Matrix4x4 mat = Gizmos.matrix;
            float angle = Mathf.PI * 2 * Mathf.Rad2Deg;
            //if (rotation == default) rotation = Quaternion.identity;

            //Gizmos�̍s���ϊ�����
            Gizmos.matrix = Matrix4x4.TRS(direction, rotation, Vector3.one);

            //�}�`��`���n�߂�n�_
            Vector3 from = direction * radius;
            //Gizmos.DrawLine(coneTip, from);

            //�����ʂ����߂�
            int addition = Mathf.RoundToInt(angle / segments);
            

            for (int i = 0; i <= angle; i += addition)  //�~��`��
            {
                float cos = radius * Mathf.Cos(i * Mathf.Deg2Rad);
                Vector3 to = new Vector3(cos, radius * Mathf.Sin(i * Mathf.Deg2Rad), 0f);
                Gizmos.DrawLine(from, to);
                Gizmos.DrawLine(coneTip, to);
                from = to;
            }

            //Gizmos�̍s������ɖ߂�
            Gizmos.matrix = mat;
        }
    }
}
