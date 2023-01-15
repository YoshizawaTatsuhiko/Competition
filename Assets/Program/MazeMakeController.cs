using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>��������x�ɕω�����}�b�v�𐶂ݏo��</summary>
public class MazeMakeController : MonoBehaviour
{
    /// <summary>���H�𐶐�����</summary>
    public void GenerateMaze(int width, int height)
    {
        if (width < 5 || height < 5) throw new System.ArgumentOutOfRangeException();
        if (width % 2 == 0) width++;
        if (height % 2 == 0) height++;
    }

    /// <summary>���H�𕶎���ɂ��ĕ\������</summary>
    /// <param name="maze">���H�̑S��</param>
    /// <returns>�����񉻂������H</returns>
    private string ArrayToString(string[,] maze)
    {
        string str = "";

        for(int i = 0; i < maze.Length; i++)
        {
            for (int j = 0; j < maze.Length; j++) str += maze[i, j];
            if (i < maze.Length - 1) str += "\n";
        }
        Debug.Log(str);
        return str;
    }
}
