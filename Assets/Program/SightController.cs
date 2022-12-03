using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SightController : MonoBehaviour
{
    [SerializeField, Tooltip("���G����Ώ�")]
    private Transform _terget;
    [SerializeField, Tooltip("���G�͈�")]
    private float _searchAngle = 1f;
    [SerializeField, Tooltip("���G����")]
    private float _range = 1f;
    [SerializeField, Tooltip("")]
    private bool _isGizmos = true;
    /// <summary>Player��F�������ǂ������ʂ���</summary>
    private bool _isDiscover = false;
    /// <summary>Player�̂������</summary>
    private Vector3 _direction;

    private void FixedUpdate()
    {
        //Player�ƃQ�[���I�u�W�F�N�g�̋�����������v�Z����
        float distance = Vector3.Distance(_terget.position, transform.position);
        _direction = _terget.position - transform.position;
        float angle = Vector3.Angle(_direction, transform.forward);

        //Player�����E�͈͓̔��ɓ��������̏���
        if (angle <= _searchAngle && distance <= _range)
        {
            transform.forward = _terget.position;
            _isDiscover = Physics.Raycast(transform.position, 
                                                  _direction, _range, LayerMask.GetMask("Player"));

            if (_isDiscover)
            {

            }
            else
            {

            }
        }
    }

    private void OnDrawGizmos()
    {
        //_isGizmos��false�̎��AGizmos���\���ɂ���
        if (_isGizmos == false) return;

        //Player�����m����Ray
        Gizmos.DrawRay(transform.position, _direction * _range);
        Handles.color = Color.red;
        //Handles.DrawSolidArc(transform.position, Vector3.up,);
    }
}
