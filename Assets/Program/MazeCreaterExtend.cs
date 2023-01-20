using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>��������x�ɍ\�����ω�����}�b�v�𐶐�����</summary>
public class MazeCreaterExtend : MonoBehaviour
{
    /// <summary>�g�����̕ǂ̏����i�[����</summary>
    Stack<(int, int)> _currentWall = new Stack<(int, int)>();
    /// <summary>�ǐ����J�n�n�_</summary>
    List<(int, int)> _startPoint = new List<(int, int)>();

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
                    // �ǐ����J�n�n�_�������X�g�ɒǉ�����B
                    if (x % 2 == 0 && y % 2 == 0)
                    {
                        _startPoint.Add((x, y));
                    }
                }
            }
        }
        ExtendWall(maze, _startPoint);

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
            List<Direction> dirs = new List<Direction>();

            if (maze[x, y - 1] == "F" && !IsCurrentWall(x, y - 2)) dirs.Add(Direction.Up);
            if (maze[x, y + 1] == "F" && !IsCurrentWall(x, y + 2)) dirs.Add(Direction.Down);
            if (maze[x - 1, y] == "F" && !IsCurrentWall(x - 2, y)) dirs.Add(Direction.Left);
            if (maze[x + 1, y] == "F" && !IsCurrentWall(x + 2, y)) dirs.Add(Direction.Right);
            // �ǂ�ݒu����
            SetWall(maze, x, y);
            int dirsIndex = Random.Range(0, dirs.Count);
            switch (dirs[dirsIndex])
            {
                case Direction.Up:
                    SetWall(maze, x, --y);
                    SetWall(maze, x, --y);
                    isFloor = maze[x, y] == "F";
                    break;
                case Direction.Down:
                    SetWall(maze, x, ++y);
                    SetWall(maze, x, ++y);
                    isFloor = maze[x, y] == "F";
                    break;
                case Direction.Left:
                    SetWall(maze, --x, y);
                    SetWall(maze, --x, y);
                    isFloor = maze[x, y] == "F";
                    break;
                case Direction.Right:
                    SetWall(maze, ++x, y);
                    SetWall(maze, ++x, y);
                    isFloor = maze[x, y] == "F";
                    break;
            }
        }

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
        // x, y�����ɋ�����������A�B
        if (x % 2 == 0 && y % 2 == 0)
        {
            _currentWall.Push((x, y));
        }
    }

    /// <summary>�g�����̕ǂ��ǂ������肷��</summary>
    /// <returns>true -> �g���� | false -> �g����</returns>
    private bool IsCurrentWall(int x, int y)
    {
        return _currentWall.Contains((x, y));
    }

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

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }

}
