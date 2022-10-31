using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>��������x�ɕω�����}�b�v�𐶂ݏo��</summary>
public class RandomMapController : MonoBehaviour
{
    /// <summary>�ǐ����J�n�n�_���</summary>
    List<(int, int)> _startPoint = new List<(int, int)>();
    /// <summary>�S�[���n�_���̍��W���i�[����</summary>
    List<(int, int)> _goalPosition = new List<(int, int)>();

    /// <summary>�}�b�v��������������</summary>
    /// <param name="width">����</param>
    /// <param name="height">�c��</param>
    public string GenerateMap(int width, int height)
    {
        //�n���ꂽ�l�� 5������������A�G���[��Ԃ�
        if (width < 5 || height < 5) throw new System.ArgumentOutOfRangeException();
        //�n���ꂽ�l����Ȃ�A�����ɂ��ĕԂ�
        int w = width % 2 == 0 ? width : width - 1;
        int h = height % 2 == 0 ? height : height - 1;

        //�}�b�v�̊O����ǂɂ��A���̓�����S�ĒʘH�Ŗ��߂�B
        string[,] maze = new string[w, h];
        for (int i = 0; i < w; i++)
            for (int j = 0; j < h; j++)
            {
                maze[i, j] = i * j == 0 || i == w - 1 || j == h - 1 ? "W" : "F";
                //�����Ԗڂ̃}�X��ǐ����J�n�n�_���ɒǉ�����
                if (i % 2 == 0 && j % 2 == 0) { _startPoint.Add((i, j)); }
            }
        ExtendWall(maze, _startPoint);
        SetSpot(maze, "S");
        SetSpot(maze, "G");
        return ArrayToString(maze);
    }

    /// <summary>�ǂ������_���ȕ����ɐL�΂�</summary>
    /// <param name="map">�}�b�v�̑傫��</param>
    /// <param name="startPoints">�ǐ����J�n�n�_���̃��X�g</param>
    void ExtendWall(string[,] map, List<(int, int)> startPoints)
    {
        //�ǐ����J�n�n�_��₩��A�����_���ɑI������
        int startIndex = Random.Range(0, startPoints.Count);
        //�ǐ����J�n�n�_���Z�b�g����
        int x = startPoints[startIndex].Item1;
        int y = startPoints[startIndex].Item2;
        //�ǐ����J�n�n�_�̃��X�g����A��₾�������W���폜����
        startPoints.RemoveAt(startIndex);

        while(true)
        {
            //�ǂ�L�΂������̌������X�g�Ɋi�[����
            List<string> direction = new List<string>();
            //�ǂ�L�΂��邩���m�F����
            if (map[x, y - 1] == "F" && map[x, y - 2] == "F") direction.Add("UP");
            if (map[x, y + 1] == "F" && map[x, y + 2] == "F") direction.Add("DOWN");
            if (map[x - 1, y] == "F" && map[x - 2, y] == "F") direction.Add("LEFT");
            if (map[x + 1, y] == "F" && map[x + 2, y] == "F") direction.Add("RIGHT");
            //�ǂ�L�΂������������_���Ō��߂�
            int dirIndex = Random.Range(0, direction.Count);
            switch (direction[dirIndex])
            {
                case "UP":
                    WallInstallation(map, x, y--);
                    WallInstallation(map, x, y--);
                    break;
                case "DOWN":
                    WallInstallation(map, x, y++);
                    WallInstallation(map, x, y++);
                    break;
                case "LEFT":
                    WallInstallation(map, x--, y);
                    WallInstallation(map, x--, y);
                    break;
                case "RIGHT":
                    WallInstallation(map, x++, y);
                    WallInstallation(map, x++, y);
                    break;
            }
            //�ǂ���������
            map[x, y] = "W";
            //�ǂ������ł���������Ȃ��Ȃ�����A���[�v�𔲂���
            if (direction.Count == 0) break;
        }
    }

    /// <summary>�ǂ�ݒu����</summary>
    void WallInstallation(string[,] map, int x, int y)
    {
        map[x, y] = "W";
        // x. y �����ɋ�����������A�ǐ����J�n�n�_��₩��폜����
        if (x % 2 == 0 && y % 2 == 0) _startPoint.Remove((x, y));
    }

    /// <summary>�����_���ȍs���~�܂�ɁA����̕�����ݒu����</summary>
    /// <param name="maze">�}�b�v�̑傫��</param>
    /// <param name="str">����̕���</param>
    void SetSpot(string[,] maze, string str)
    {
        foreach (var point in _goalPosition.
            OrderBy(_ => _goalPosition.Count).Where(p => maze[p.Item1, p.Item2] == "F"))
        {
            //3�������ǂɂȂ��Ă���ꏊ��T��
            int count = 0;
            if (maze[point.Item1, point.Item2 - 1] == "W") count++;
            if (maze[point.Item1, point.Item2 + 1] == "W") count++;
            if (maze[point.Item1 - 1, point.Item2] == "W") count++;
            if (maze[point.Item1 + 1, point.Item2] == "W") count++;

            if(count == 3)
            {
                maze[point.Item1, point.Item2] = str;
                Debug.Log($"{str} = Complete");
                break;
            }
        }
    }

    /// <summary>���H�𕶎���ɂ��ĕ\������</summary>
    /// <param name="maze">���H�̑S��</param>
    /// <returns>�����񉻂������H</returns>
    string ArrayToString(string[,] maze)
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
