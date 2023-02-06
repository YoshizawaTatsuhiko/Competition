using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _gameClear = new();
    [SerializeField]
    private UnityEvent _gameOver = new();
    private GameObject _player = null;
    /// <summary>�Q�[�����n�܂�������Player�𐶐�������W</summary>
    private Vector3 _startPoint = new();
    /// <summary>Goal���o�����邽�߂ɋN������Gimmick</summary>
    private GimmickCheck _gimmick = null;
    private GoalController _goal = null;

    private void Start()
    {
        // �Q�[���J�n���Ƀ}�E�X�J�[�\���������Ȃ�����B
        Cursor.visible = false;

        // �Q�[���J�n�n�_�̍��W�������Ă���
        _startPoint = GameObject.FindGameObjectWithTag("StartPoint").transform.position;
        _startPoint.y += 1f;

        // Resources�t�H���_�[����Player�𐶐�����B
        _player = Instantiate(Resources.Load<GameObject>("Player Prefab"), _startPoint, Quaternion.identity);
        Instantiate(Resources.Load<GameObject>("Marker"));

        _gimmick = FindObjectOfType<GimmickCheck>();
        _goal = FindObjectOfType<GoalController>();
        _goal.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        // �N������Gimmick�̐�����萔�ɒB������AGoal���o��������B
        if (_gimmick.GimmickWakeUpCount == 2)
        {
            _goal.gameObject.SetActive(true);
        }

        if (_goal.GoalJudge)
        {
            GameClear();
        }
    }

    /// <summary>�Q�[���N���A��������Event���Ă�</summary>
    private void GameClear()
    {
        _gameClear.Invoke();
        Cursor.visible = true;
        Debug.Log("Complete");
    }

    /// <summary>�Q�[���I�[�o�[��������Event���Ă�</summary>
    private void GameOver()
    {
        _gameOver.Invoke();
    }
}
