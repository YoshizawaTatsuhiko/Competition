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
    //[SerializeField, Tooltip("Gizmos��\�����邩�ǂ���\ntrue -> �\�� | false -> ��\��")]
    //private bool _isGizmos = true;
    /// <summary>�W�I�̂������</summary>
    private float _distance;

    private void Start()
    {
        if (!_terget) Debug.LogWarning("�^�[�Q�b�g��assign����Ă��܂���B");
    }

    private void FixedUpdate()
    {
        _distance = Vector3.Distance(_terget.transform.position, transform.position);

        if (SearchTerget(_searchAngle) && _distance <= _range)
        {
            Debug.Log("�ʉ�");
            if (LookAtTerget())
            {
                Debug.Log("���O�����Ă���");
            }
        }
        else
        {
            Debug.Log("(�ɂ��Ȃ�...)");
        }
    }

    /// <summary>Terget�����E�ɓ����Ă��邩�ǂ������肷��</summary>
    /// <param name="angle">���G�͈�</param>
    /// <returns>�����Ă��� -> true | �����Ă��Ȃ� -> false;</returns>
    private bool SearchTerget(float angle)
    {
        //Terget�ƃQ�[���I�u�W�F�N�g�̓��ς��v�Z����
        float dot = Vector3.Dot(transform.forward, (_terget.transform.position - transform.position).normalized);

        //Player�����E�͈͓̔��ɓ��������̏���
        if(dot > angle)
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
    /// <returns>��Q���i�V -> true | ��Q���A�� -> false</returns>
    private bool LookAtTerget()
    {
        Debug.DrawRay(transform.position, transform.forward * _range);

        if (Physics.Raycast(transform.position, transform.forward, _range, _terget.gameObject.layer))
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
