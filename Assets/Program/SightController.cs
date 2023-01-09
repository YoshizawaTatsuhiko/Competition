using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MyGizmos;  //���얼�O��ԁFGizmos��ǉ�����

public class SightController : MonoBehaviour
{
    [SerializeField, Tooltip("���G����Ώ�")]
    private Transform _terget;
    [SerializeField, Range(0f, 1f), Tooltip("���G�͈�")]
    private float _searchAngle = 1f;
    [SerializeField, Tooltip("���G����")]
    private float _range = 1f;
    /// <summary>�ΏۂƂ̋���</summary>
    private float _distance;
    /// <summary>�Ώۂ𔭌��������ǂ����𔻒肷��</summary>
    private bool _isDiscover = false;
    /// <summary>�Ώی����������ǂ����𔻒肷��</summary>
    private bool _isTergetLost = false;
    /// <summary>�Ώۂ������������Ɍ����������</summary>
    private Vector3 _front = Vector3.zero;
    /// <summary>�U������܂ł̎���</summary>
    private float _turnTime = 0.5f;

    private void Start()
    {
        if (!_terget) Debug.LogWarning("�Ώۂ�assign����Ă��܂���B");
    }

    private void FixedUpdate()
    {
        //_distance = Vector3.Distance(_terget.transform.position, transform.position);

        //�Ώۂ𔭌�������
        if(_isDiscover)
        {
            _front = transform.forward;

            //Ray���΂��āA�ΏۂƂ̊Ԃɏ�Q�������邩�ǂ������ׂ�
            if (Physics.Raycast(transform.position, transform.forward * _range, _terget.gameObject.layer))
            {
                Debug.DrawRay(transform.position, transform.forward * _range);
                transform.LookAt(Vector3.Lerp(transform.forward + transform.position, _terget.position, _turnTime));
                Debug.Log("Look...");
            }
            else
            {
                _isDiscover = false;
                _isTergetLost = true;
                Debug.Log("Switch");
            }
        }

        //�Ώۂ𔭌��ł��Ă��Ȃ���
        else
        {
            //�Ώۂ𔭌�����Ƃ��Ɏg������
            float discoverDot = Mathf.Abs(Vector3.Dot(
                transform.forward, (_terget.position - transform.position).normalized));

            if (discoverDot >= _searchAngle)
            {
                _isDiscover = true;
                Debug.Log("Discover!");
            }
            if (_isTergetLost)
            {
                //�Ώۂ������������A��������ȑO�̕����������Ƃ��Ɏg������
                float lookDot = Vector3.Dot(transform.forward, _front);

                if (lookDot <= 0.95f)
                {
                    transform.LookAt(
                        Vector3.Lerp(transform.forward + transform.position, _front + transform.position, _turnTime));
                }
                else  //������x�����O�̕�������������A�U������̂���߂�
                {
                    _isTergetLost = false;
                }
            }
            //Debug.Log("(�ɂ��Ȃ�...)");
        }
    }

    /*
    /// <summary>Terget�����E�ɓ����Ă��邩�ǂ������肷��</summary>
    /// <param name="angle">���G�͈�</param>
    /// <returns>�����Ă��� -> true | �����Ă��Ȃ� -> false;</returns>
    private bool SearchTerget(float angle)
    {
        //Terget�ƃQ�[���I�u�W�F�N�g�̓��ς��v�Z����
        float dot = Vector3.Dot(transform.forward, (_terget.position - transform.position).normalized);

        //Player�����E�͈͓̔��ɓ��������̏���
        if(Mathf.Abs(dot) >= angle)
        {
            return true;
        }

        //Player�����E�͈̔͊O�ɋ��鎞�̏���
        else
        {
            return false;
        }
    }

    /// <summary>�^�[�Q�b�g�ƃQ�[���I�u�W�F�N�g�̊Ԃɏ�Q�������邩�ǂ����𔻒肷��</summary>
    /// <param name="terget">�Î�����Ώ�</param>
    /// <returns>��Q���i�V -> true | ��Q���A�� -> false</returns>
    private void LookAtTerget(Transform terget)
    {
        //�^�[�Q�b�g�̕���������
        transform.LookAt(Vector3.Lerp(transform.forward + transform.position, terget.position, 1f));

        //�ΏۂƃQ�[���I�u�W�F�N�g�̊Ԃɏ�Q�������邩�ǂ������m�F���邽�߂�Ray���΂�
        Physics.Raycast(transform.position, transform.forward, terget.gameObject.layer);
        Debug.DrawRay(transform.position, transform.forward);
    }
    */
}
