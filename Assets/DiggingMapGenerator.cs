using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// ���@��@��p���ă}�b�v�𐶐�����
/// </summary>
public class DiggingMapGenerator : MonoBehaviour
{
    /// <summary>�����@��J�n�n�_�ƂȂ���W�̃��X�g</summary>
    List<(int, int)> _startMasses = new List<(int, int)>();
    /// <summary>�S�[����ݒu������ƂȂ�}�X�̃��X�g</summary>
    List<(int, int)> _goalMasses = new List<(int, int)>();

    /// <summary>���ƍ����ɉ������}�b�v�𐶐����A������ɂ��ĕԂ�</summary>
    public string GenerateRandomMap(int width, int height)
    {
        // �n���ꂽ���������Ȃ�-1���Ċ�ɒ���
        int h = height % 2 != 0 ? height : height - 1;
        int w = width % 2 != 0 ? width : width - 1;
        // �O����ʘH�A����ȊO��ǂɂ���
        string[,] map = new string[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                map[i, j] = i * j == 0 || i == h - 1 || j == w - 1 ? "O" : "W";
        // �����@��
        _startMasses.Add((1, 1));
        // ��{���������ۂɃS�[���܂��̓X�^�[�g����������Ȃ��̂�h�����߂ɃS�[���}�X�Ƃ��Ă��ǉ�
        _goalMasses.Add((1, 1));
        DiggingPath(map, _startMasses);
        // �O����ǂɖ߂�
        for (int i = 0; i < /*map.GetLength(0)*/ h; i++)
        {
            map[i, 0] = "W";
            map[i, /*map.GetLength(1)*/ h - 1] = "W";
        }
        for (int i = 0; i < /*map.GetLength(1)*/ w; i++)
        {
            map[0, i] = "W";
            map[/*map.GetLength(0)*/ w - 1, i] = "W";
        }
        // ���H��ݒu����
        SetWaterPath(map, w, h, 10);
        // �S�[���ƃX�^�[�g��ݒu����
        SetSpotRandom(map, "E");
        SetSpotRandom(map, "P");

        return ArrayToString(map);
    }

    /// <summary>�ʘH���@��</summary>
    void DiggingPath(string[,] map, List<(int, int)> startMasses)
    {
        // �J�n���W�̃��X�g�̒����烉���_���Ɍ���
        int startIndex = Random.Range(0, startMasses.Count);
        // �J�n���W���Z�b�g
        int x = startMasses[startIndex].Item1;
        int y = startMasses[startIndex].Item2;
        // �J�n���W�̃��X�g����폜����
        startMasses.RemoveAt(startIndex);

        while (true)
        {
            // �@��i�߂�����𕶎���Ŋi�[���郊�X�g
            List<string> dirs = new List<string>();
            // �㉺���E�A2�}�X��܂ŕǂ��ǂ������ׂ�
            if (map[x, y - 1] == "W" && map[x, y - 2] == "W")
                dirs.Add("Up");
            if (map[x, y + 1] == "W" && map[x, y + 2] == "W")
                dirs.Add("Down");
            if (map[x - 1, y] == "W" && map[x - 2, y] == "W")
                dirs.Add("Left");
            if (map[x + 1, y] == "W" && map[x + 2, y] == "W")
                dirs.Add("Right");
            // �@���������Ȃ���΃��[�v�𔲂���
            if (dirs.Count == 0) break;
            // �J�n���W���@��
            map[x, y] = "O";
            // �@������������_���Ɍ��߂�
            int dirIndex = Random.Range(0, dirs.Count);
            switch (dirs[dirIndex])
            {
                case "Up":
                    DiggingMass(map, x, --y);
                    DiggingMass(map, x, --y);
                    break;
                case "Down":
                    DiggingMass(map, x, ++y);
                    DiggingMass(map, x, ++y);
                    break;
                case "Left":
                    DiggingMass(map, --x, y);
                    DiggingMass(map, --x, y);
                    break;
                case "Right":
                    DiggingMass(map, ++x, y);
                    DiggingMass(map, ++x, y);
                    break;
            }
        }

        // ���݂̍��W���S�[���̌��}�X�̃��X�g�ɒǉ�����
        _goalMasses.Add((x, y));

        // �����J�n���W�̃��X�g�̒��g������Ȃ炻������ʘH���@��
        if (startMasses.Count > 0)
            DiggingPath(map, startMasses);
    }

    /// <summary>�w�肳�ꂽ�}�X���@��</summary>
    void DiggingMass(string[,] map, int x, int y)
    {
        map[x, y] = "O";
        // �������̍��W��x,y���Ɋ�Ȃ�J�n���W�̃��X�g�ɒǉ�����
        if (x * y % 2 != 0)
            _startMasses.Add((x, y));
    }

    /// <summary>
    /// ���H������
    /// </summary>
    /// <param name="tri">���s��(�傫����ΐ��H��������\�����オ��)</param>
    void SetWaterPath(string[,] map, int width, int height, int tri)
    {
        for (int i = 0; i < tri; i++)
        {
            int baseX = Random.Range(0, width / 2) * 2;
            int baseY = Random.Range(0, height / 2) * 2;
            for (int j = 0; j < 2; j++)
            {
                (int, int)[] pair = { (1, 0), (-1, 0), (0, 1), (0, -1) };
                int dr = Random.Range(0, 4);

                int addX = pair[dr].Item1;
                int addY = pair[dr].Item2;

                while (baseX + addX >= 0 && baseX + addX < width - 1 &&
                       baseY + addY >= 0 && baseY + addY < height - 1)
                {
                    //int xx = baseX + addX;
                    //int yy = baseX + addY;
                    //Debug.Log($"x��0�`{width / 2 * 2}�Ay��0�`{height / 2 * 2}�A��({xx},{yy})");
                    if (map[baseY + addY, baseX + addX] == "O")
                    {
                        break;
                    }
                    map[baseY + addY, baseX + addX] = "S";
                    addX += pair[dr].Item1;
                    addY += pair[dr].Item2;
                }
            }
        }
    }

    /// <summary>�����_���ȍs���~�܂�̈ʒu�ɔC�ӂ̕�����ݒu����</summary>
    void SetSpotRandom(string[,] map, string Char)
    {
        // �S�[�����̃}�X�̃��X�g�̒����珰�̃}�X��T��
        foreach ((int, int) mass in _goalMasses
            .OrderBy(_ => System.Guid.NewGuid())
            .Where(i => map[i.Item1, i.Item2] == "O"))
        {
            // 3�������ǂɂȂ��Ă���}�X��T��
            int count = 0;
            if (map[mass.Item1 - 1, mass.Item2] == "W" ||
                map[mass.Item1 - 1, mass.Item2] == "S") count++;
            if (map[mass.Item1 + 1, mass.Item2] == "W" ||
                map[mass.Item1 + 1, mass.Item2] == "S") count++;
            if (map[mass.Item1, mass.Item2 - 1] == "W" ||
                map[mass.Item1, mass.Item2 - 1] == "S") count++;
            if (map[mass.Item1, mass.Item2 + 1] == "W" ||
                map[mass.Item1, mass.Item2 + 1] == "S") count++;

            if (count == 3)
            {
                map[mass.Item1, mass.Item2] = Char;
                Debug.Log(Char + "�𐶐����܂���");
                break;
            }
        }
    }

    /// <summary>�񎟌��z��𕶎���ɂ��ĕԂ�</summary>
    string ArrayToString(string[,] array)
    {
        string str = "";
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
                str += array[i, j];
            if (i < array.GetLength(0) - 1)
                str += '\n';
        }
        Debug.Log(str); // �f�o�b�O�p�Ɏc���Ă���
        return str;
    }
}
