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

#if UNITY_EDITOR
    
#endif


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

    public bool SearchTerget()
    {
        //Player�ƃQ�[���I�u�W�F�N�g�̋�����������v�Z����
        float distance = Vector3.Distance(_terget.position, transform.position);
        _direction = _terget.position - transform.position;
        float angle = Vector3.Angle(_direction, transform.forward);

        //Player�����E�͈͓̔��ɓ��������̏���
        if (angle <= _searchAngle && distance <= _range)
        {
            transform.forward = _direction;
            //_isDiscover = Physics.Raycast(transform.position,
            //                                      _direction, _range, LayerMask.GetMask("Player"));
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
        Handles.color = color;
        Handles.DrawSolidArc(transform.position, transform.forward, 
            new Vector3(/*[X]*/ _searchAngle / 2f + transform.forward.x, /*[Y]*/ 0f,
                        /*[Z]*/ _searchAngle / 2f + transform.forward.z).normalized, 
            Mathf.PI * 2 * Mathf.Rad2Deg, _range);
    }
}
