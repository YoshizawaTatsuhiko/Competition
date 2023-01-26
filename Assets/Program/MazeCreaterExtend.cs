using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>��������x�ɍ\�����ω�����}�b�v�𐶐�����</summary>
public class MazeCreaterExtend : MonoBehaviour
{
    /// <summary>�ǐ����J�n�n�_</summary>
    List<(int, int)> _startPoint = new List<(int, int)>();
    /// <summary>�g�����̕ǂ̏����i�[����</summary>
    Stack<(int, int)> _currentWall = new Stack<(int, int)>();
    /// <summary>�C�ӂ̃C�x���g���N�������W�����郊�X�g</summary>
    List<(int, int)> _coordinateList = new List<(int, int)>();

    #region Maze Generation Algorithm

    /// <summary>���H�𐶐�����</summary>
    public string GenerateMaze(int width, int height)
    {
        // �c���̑傫����5�����������琶�����Ȃ��B
        if (width < 5 || height < 5) throw new System.ArgumentOutOfRangeException();
        // �c(��)�̒l��������������A��ɕϊ�����B
        width = width % 2 == 0 ? width + 1 : width;
        height = height % 2 == 0 ? height + 1 : height;

        // ���H�̏����i�[����B
        string[,] maze = new string[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // �O����ǂň͂ށB
                if (x * y == 0 || x == width - 1 || y == height - 1)
                {
                    maze[x, y] = "W";
                }
                // �O���ȊO�͏��Ŗ��߂�B
                else
                {
                    maze[x, y] = "F";
                    // �ǐ����J�n���W�������X�g�ɒǉ�����B
                    if (x % 2 == 0 && y % 2 == 0)
                    {
                        _startPoint.Add((x, y));
                    }
                }
            }
        }
        ExtendWall(maze, _startPoint);
        _coordinateList = FindCoordinate(maze);
        (int, int) point = SetSpotRandom(maze, _coordinateList, "S");
        FindFurthestPoint(maze, point, _coordinateList, "G");
        SetSpotRandom(maze, _coordinateList, "C");
        SetSpotRandom(maze, _coordinateList, "C");

        return ArrayToString(maze);
    }

    /// <summary>�ǂ��g������</summary>
    private void ExtendWall(string[,] maze, List<(int, int)> startPoint)
    {
        int index = Random.Range(0, startPoint.Count);
        int x = startPoint[index].Item1;
        int y = startPoint[index].Item2;
        startPoint.RemoveAt(index);
        bool isFloor = true;

        while (isFloor)
        {
            // �g���ł���������i�[���郊�X�g
            List<string> dirs = new List<string>();

            if (maze[x, y - 1] == "F" && !IsCurrentWall(x, y - 2)) dirs.Add("Up");
            if (maze[x, y + 1] == "F" && !IsCurrentWall(x, y + 2)) dirs.Add("Down");
            if (maze[x - 1, y] == "F" && !IsCurrentWall(x - 2, y)) dirs.Add("Left");
            if (maze[x + 1, y] == "F" && !IsCurrentWall(x + 2, y)) dirs.Add("Right");
            // �g�����������������Ȃ�������A���[�v�𔲂���
            if (dirs.Count == 0) break;
            // �ǂ�ݒu����
            SetWall(maze, x, y);
            int dirsIndex = Random.Range(0, dirs.Count);
            try
            {
                switch (dirs[dirsIndex])
                {
                    case "Up":
                        isFloor = maze[x, y - 2] == "F";
                        SetWall(maze, x, --y);
                        SetWall(maze, x, --y);
                        break;
                    case "Down":
                        isFloor = maze[x, y + 2] == "F";
                        SetWall(maze, x, ++y);
                        SetWall(maze, x, ++y);
                        break;
                    case "Left":
                        isFloor = maze[x - 2, y] == "F";
                        SetWall(maze, --x, y);
                        SetWall(maze, --x, y);
                        break;
                    case "Right":
                        isFloor = maze[x + 2, y] == "F";
                        SetWall(maze, ++x, y);
                        SetWall(maze, ++x, y);
                        break;
                }
            }
            catch (System.Exception e)
            {
                Debug.Log(e.ToString());
            }
        }
        // �g���ł���|�C���g���܂���������g���𑱂���B
        if (startPoint.Count > 0)
        {
            _currentWall.Clear();
            ExtendWall(maze, startPoint);
        }
    }

    /// <summary>�ǂ�ݒu����</summary>
    private void SetWall(string[,] maze, int x, int y)
    {
        maze[x, y] = "W";
        // x, y�����ɋ�����������A���X�g����폜���A�X�^�b�N�Ɋi�[����B
        if (x % 2 == 0 && y % 2 == 0)
        {
            _startPoint.Remove((x, y));
            _currentWall.Push((x, y));
        }
    }

    /// <summary>�g�����̕ǂ��ǂ������肷��</summary>
    /// <returns>true -> �g���� | false -> �g����</returns>
    private bool IsCurrentWall(int x, int y)
    {
        return _currentWall.Contains((x, y));
    }

    #endregion

    #region Event Method

    /// <summary>3�������ǂɂȂ��Ă�����W��������</summary>
    /// <returns>3�������ǂɂȂ��Ă�����W�̃��X�g�u(int, int)�^�v</returns>
    private List<(int, int)> FindCoordinate(string[,] maze)
    {
        // �����ɍ��v�������W���i�[���郊�X�g
        List<(int, int)> coordinateList = new List<(int, int)>();

        // �A���S���Y���̓s����Ai * j == ��̏ꏊ���������ɍ������W�͑��݂��Ȃ��̂Ŋ�Ԗڂ̍��W�̂݌�������B
        for (int i = 1; i < maze.GetLength(1); i += 2)
        {
            for (int j = 1; j < maze.GetLength(0); j += 2)
            {
                // �אڂ���4�����̂ǂꂩ���ǂ�������A�J�E���g����B
                int count = 0;

                if (maze[i, j - 1] == "W") count++;
                if (maze[i, j + 1] == "W") count++;
                if (maze[i - 1, j] == "W") count++;
                if (maze[i + 1, j] == "W") count++;

                if (count == 3)
                {
                    coordinateList.Add((i, j));
                }
            }
        }
        return coordinateList;
    }

    /// <summary>����̍��W����ł��������W�������A������z�u����</summary>
    /// <param name="point">��ƂȂ���W</param>
    /// <param name="coordinateList">������z�u��������W�̃��X�g</param>
    /// <param name="chara">�z�u���镶��</param>
    private void FindFurthestPoint(string[,] maze, (int, int) point, List<(int, int)> coordinateList, string chara)
    {
        if (coordinateList.Count == 0)
        {
            Debug.LogWarning("���n�_��������܂���ł����B");
            return;
        }

        int max = int.MinValue;
        (int, int) tapple = (0, 0);

        foreach ((int, int) n in coordinateList)
        {
            // �s�^�S���X�̒藝���g���āA�ł��������W����������B
            int distance = 
                (n.Item1 - point.Item1) * (n.Item1 - point.Item1) + (n.Item2 - point.Item2) * (n.Item2 - point.Item2);

            // �ł��������W�̎b��1�ʂ��X�V���Ă����B
            if(max < distance)
            {
                max = distance;
                tapple = n;
            }
        }
        // ����̍��W�ɕ�����z�u������A���̍��W�����X�g����폜����B
        // ����ɂ��A�������X�g�g���Ă������A�㏑������邱�Ƃ͖����Ȃ�B
        maze[tapple.Item1, tapple.Item2] = chara;
        coordinateList.Remove(tapple);
    }

    /// <summary>�C�ӂ̕����������_���ȍ��W�ɔz�u����</summary>
    /// <param name="coordinateList">������z�u��������W�̃��X�g</param>
    /// <param name="chara">�z�u���镶��</param>
    private (int, int) SetSpotRandom(string[,] maze, List<(int, int)> coordinateList, string chara)
    {
        (int, int) tapple = (0, 0);

        // �����W�̃��X�g�̗v�f��GUID���ꎞ�I�Ɋ��蓖�ĂāA�\�[�g����B
        // GUID�̒l�̓����_���Ȃ̂ŁA�v�f�̏��Ԃ��o���o���ɂȂ�B
        foreach ((int, int) p in coordinateList.OrderBy(_ => System.Guid.NewGuid()))
        {
            maze[p.Item1, p.Item2] = chara;
            tapple = p;
            coordinateList.Remove((p.Item1, p.Item2));
            break;
        }
        return tapple;
    }

    #endregion

    /// <summary>���H�𕶎���ɂ��ĕ\������</summary>
    /// <param name="maze">���H</param>
    /// <returns>�����񉻂������H</returns>
    private string ArrayToString(string[,] maze)
    {
        string str = "";

        for(int i = 0; i < maze.GetLength(1); i++)
        {
            for (int j = 0; j < maze.GetLength(0); j++)
            {
                str += maze[i, j];
            }
            if (i < maze.Length - 1) str += "\n";
        }
        Debug.Log(str);
        return str;
    }
}
