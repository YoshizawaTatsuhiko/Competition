using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MazeCreaterExtend))]

class MazeGenerator : MonoBehaviour
{
    [SerializeField, Tooltip("MAX SIZE = 32")]
    private int _size = 5;
    public int Width { get => _size < 5 ? 5 : _size; }
    public int Height { get => _size < 5 ? 5 : _size; }

    [SerializeField]
    private GameObject _wall = null;
    [SerializeField]
    private GameObject _path = null;
    /// <summary>��������Ƃ��̒ʘH�̍��W</summary>
    private float _pathHeight = 0f;
    [SerializeField]
    private GameObject _start = null;
    [SerializeField]
    private GameObject _goal = null;
    [SerializeField]
    private GameObject _gimic = null;
    [SerializeField]
    private GameObject _ceiling = null;
    /// <summary>�����𐶐�������W(���H�𐶐����Ă��牮�������Ԃ��邽��)</summary>
    private Vector3 _ceilingPos = Vector3.zero;
    /// <summary>�쐬�������H</summary>
    private MazeCreaterExtend _maze = null;
    /// <summary>���H�̐݌v�}</summary>
    private string[,] _bluePrint;
    /// <summary>�C�ӂ̃C�x���g���N�������W�����郊�X�g</summary>
    List<(int, int)> _coordinateList = new List<(int, int)>();

    private void Awake()
    {
        _maze = GetComponent<MazeCreaterExtend>();
        string[] mazeInfo = _maze.GenerateMaze(Width, Height).Split("\n");
        _bluePrint = new string[mazeInfo[0].Length, mazeInfo.Length - 1];
        To2DArray(mazeInfo, _bluePrint);

        _coordinateList = FindMazePoint(_bluePrint, "W", 3);

        (int, int) point = SetSpotRandom(_bluePrint, _coordinateList, "S");
        FindFurthestPoint(_bluePrint, point, _coordinateList, "G");

        SetSpotRandom(_bluePrint, _coordinateList, "C");
        SetSpotRandom(_bluePrint, _coordinateList, "C");

        GameObject wallParent = new GameObject("Wall Parent");
        GameObject floorParent = new GameObject("Floor Parent");
        GameObject otherParent = new GameObject("Other Parent");

        _pathHeight = Vector3.zero.y + -_wall.transform.localScale.y / 2f;

        for (int i = 0; i < _bluePrint.GetLength(0); i++)
        {
            for (int j = 0; j < _bluePrint.GetLength(1); j++)
            {
                if (_bluePrint[i, j] == "W") Instantiate(_wall,
                    new Vector3(i - Width / 2, 0f, j - Height / 2), Quaternion.identity, wallParent.transform);
                if (_bluePrint[i, j] == "F") Instantiate(_path,
                    new Vector3(i - Width / 2, _pathHeight, j - Height / 2), Quaternion.identity, floorParent.transform);
                if (_bluePrint[i, j] == "S") Instantiate(_start,
                    new Vector3(i - Width / 2, _pathHeight, j - Height / 2), Quaternion.identity, otherParent.transform);
                if (_bluePrint[i, j] == "G") Instantiate(_goal,
                    new Vector3(i - Width / 2, _pathHeight, j - Height / 2), Quaternion.identity, otherParent.transform);
                if (_bluePrint[i, j] == "C") Instantiate(_gimic,
                    new Vector3(i - Width / 2, _pathHeight, j - Height / 2), Quaternion.identity, otherParent.transform);
            }
        }
        _ceilingPos.y = Vector3.zero.y + _wall.transform.localScale.y / 2f;
        GameObject ceiling = Instantiate(_ceiling, _ceilingPos, Quaternion.identity, otherParent.transform);
        ceiling.transform.localScale = new Vector3(Width, 0.1f, Height);
    }

    /// <summary>�ꎟ���z���񎟌��z��ɕϊ�����(string�^����)</summary>
    /// <param name="array">string�^�̈ꎟ���z��</param>
    /// <param name="twoDimensionalArray">string�^�̓񎟌��z��</param>
    private string[,] To2DArray(string[] array, string[,] twoDimensionalArray)
    {
        for (int i = 0; i < twoDimensionalArray.GetLength(0); i++)
        {
            for (int j = 0; j < twoDimensionalArray.GetLength(1); j++)
            {
                twoDimensionalArray[i, j] = array[i][j].ToString();
            }
        }
        return twoDimensionalArray;
    }

    #region Event Method

    /// <summary>�אڂ����������������āA�����ɍ��v�������W�����X�g�A�b�v����</summary>
    /// <param name="conditionChar">�m�肽�����W�ɗאڂ��镶��</param>
    /// <param name="conditionCount">�אڂ���A�����ƂȂ镶���̌�</param>
    /// <returns>�����ɍ��v�������W���i�[�������X�g�u(int, int)�^�v</returns>
    private List<(int, int)> FindMazePoint(string[,] bluePrint, string conditionChar, int conditionCount)
    {
        // �����ɍ��v�������W���i�[���郊�X�g
        List<(int, int)> coordinateList = new List<(int, int)>();
        if (conditionCount < 0) conditionCount = 0;
        if (conditionCount > 4) conditionCount = 4;

        // �A���S���Y���̓s����Ai * j == ��̏ꏊ���������ɍ������W�͑��݂��Ȃ��̂Ŋ�Ԗڂ̍��W�̂݌�������B
        for (int i = 1; i < bluePrint.GetLength(1) - 1; i += 2)
        {
            for (int j = 1; j < bluePrint.GetLength(0) - 1; j += 2)
            {
                // �אڂ���4�����̂ǂꂩ���ǂ�������A�J�E���g����B
                int count = 0;

                if (bluePrint[i, j - 1] == conditionChar) count++;
                if (bluePrint[i, j + 1] == conditionChar) count++;
                if (bluePrint[i - 1, j] == conditionChar) count++;
                if (bluePrint[i + 1, j] == conditionChar) count++;

                if (count == conditionCount)
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
    private void FindFurthestPoint(string[,] bluePrint, (int, int) point, List<(int, int)> coordinateList, string chara)
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
            if (max < distance)
            {
                max = distance;
                tapple = n;
            }
        }
        // ����̍��W�ɕ�����z�u������A���̍��W�����X�g����폜����B
        // ����ɂ��A�������X�g�g���Ă������A�㏑������邱�Ƃ͖����Ȃ�B
        bluePrint[tapple.Item1, tapple.Item2] = chara;
        coordinateList.Remove(tapple);
    }

    /// <summary>�C�ӂ̕����������_���ȍ��W�ɔz�u����</summary>
    /// <param name="coordinateList">������z�u��������W�̃��X�g</param>
    /// <param name="chara">�z�u���镶��</param>
    private (int, int) SetSpotRandom(string[,] bluePrint, List<(int, int)> coordinateList, string chara)
    {
        (int, int) tapple = (0, 0);

        // �����W�̃��X�g�̗v�f��GUID���ꎞ�I�Ɋ��蓖�ĂāA�\�[�g����B
        // GUID�̒l�̓����_���Ȃ̂ŁA�v�f�̏��Ԃ��o���o���ɂȂ�B
        foreach ((int, int) p in coordinateList.OrderBy(_ => System.Guid.NewGuid()))
        {
            bluePrint[p.Item1, p.Item2] = chara;
            tapple = p;
            coordinateList.Remove((p.Item1, p.Item2));
            break;
        }
        return tapple;
    }

    #endregion
}
