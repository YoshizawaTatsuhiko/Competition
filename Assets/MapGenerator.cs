using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    List<(int, int)> _startPoint = new List<(int, int)>();
    List<(int, int)> _goalPoint = new List<(int, int)>();
    /// <summary>�}�b�v�������_����������</summary>
    /// <param name="width">����</param>
    /// <param name="height">�c��</param>
    public void CreateMap(int width, int height)
    {
        //�n���ꂽ�l����Ȃ�A�����ɂ��ĕԂ�
        int w = width % 2 == 0 ? width : width - 1;
        int h = height % 2 == 0 ? height : height - 1;

        for (int i = 0; i < w; i++)
            for (int j = 0; j < h; j++)
            {

            }
    }
}
