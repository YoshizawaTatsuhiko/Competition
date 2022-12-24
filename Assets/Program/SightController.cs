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
    [SerializeField, Tooltip("Gizmos��\�����邩�ǂ���\ntrue -> �\�� | false -> ��\��")]
    private bool _isGizmos = true;
    /// <summary>�W�I�̂������</summary>
    private Vector3 _direction;

    private void FixedUpdate()
    {
        if (SearchTerget())
        {
            Debug.Log("���O�����Ă���");
        }
        else
        {
            Debug.Log("(�ɂ��Ȃ�...)");
        }
    }

    /// <summary>Terget�����E�ɓ����Ă��邩�ǂ������肷��</summary>
    /// <returns>�����Ă��� -> true | �����Ă��Ȃ� -> false;</returns>
    public bool SearchTerget()
    {
        //Terget�ƃQ�[���I�u�W�F�N�g�̋�����������v�Z����
        float distance = Vector3.Distance(_terget.position, transform.position);
        _direction = _terget.position - transform.position;
        float angle = Vector3.Angle(_direction, transform.forward);

        //Player�����E�͈͓̔��ɓ��������̏���
        if (angle <= _searchAngle && distance <= _range)
        {
            transform.forward = _direction;
            return true;
        }

        //Player�����E�͈̔͊O�ɋ��鎞�̏���
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        //_isGizmos��false�̎��AGizmos���\���ɂ���
        if (_isGizmos == false) return;

        //Player�����m����Ray
        Gizmos.DrawRay(transform.position, _direction * _range);

        //���E��\������
        Color color = new Color(1f, 0f, 0f, 0.2f);
        //Handles.color = color;
        //Handles.DrawSolidArc(transform.position, transform.forward,
        //    transform.forward * _searchAngle,
        //    Mathf.PI * 2 * Mathf.Rad2Deg, _range);
        MyGizmos.AddGizmos.DrawWireCone(transform.position, transform.forward * _range, Vector3.up, _searchAngle);
    }
}
