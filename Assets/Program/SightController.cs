using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MyGizmos;  //���얼�O��ԁFGizmos��ǉ�����

public class SightController : MonoBehaviour
{
    [SerializeField]
    private Transform _target = null;
    [SerializeField, Range(0f, 1f)]
    private float _searchDeg = 1f;
    [SerializeField]
    private float _searchDistance = 1f;
    // �^�[�Q�b�g�Ƃ̋���
    private float _distanceToTarget = 0f;
    // �^�[�Q�b�g�𔭌��������ǂ����𔻒肷��
    private bool _isFind = false;
    // �^�[�Q�b�g�������������ǂ����𔻒肷��
    private bool _isTergetLost = false;
    // �^�[�Q�b�g�������������Ɍ����������
    private Vector3 _turnAroundDir = Vector3.zero;

    private void Start()
    {
        if (!_target) Debug.LogWarning("�Ώۂ�assign����Ă��܂���B");
    }

    private void FixedUpdate()
    {
        Vector3 toTarget = transform.position - _target.position;
        float dot = Vector3.Dot(transform.forward, toTarget);
    }
}
