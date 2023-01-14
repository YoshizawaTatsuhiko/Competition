using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGizmos;  //���얼�O��ԁF�M�Y����ǉ�����

public class SightController : MonoBehaviour
{
    [SerializeField]
    private Transform _target = null;
    [SerializeField, Range(0f, 1f), Tooltip("0 -> 1 �ɂȂ�قǁA���E�������Ȃ�")]
    private float _searchAngle = 1f;
    [SerializeField]
    private float _searchRange = Mathf.Infinity;
    /// <summary>�^�[�Q�b�g�������������Ɍ����������</summary>
    private Vector3 _turnAroundDir = Vector3.zero;

    private void Start()
    {
        if (!_target) Debug.LogWarning("�Ώۂ�assign����Ă��܂���B");
    }

    private void FixedUpdate()
    {
        if (TargetSearch(_target, _searchAngle, _searchRange))
        {
            if (TargetLook(_target, _searchRange))
            {
                Debug.DrawRay(transform.position, transform.forward * _searchRange, Color.red);
                Debug.Log("LOOK");
            }
            else
            {
                Debug.Log("LOST");
            }
        }
    }

    /// <summary>���G����</summary>
    /// <param name="target">���G�Ώ�</param>
    /// <param name="angle">���G�͈�</param>
    /// <param name="range">���G����</param>
    /// <returns>true -> �������� | false -> �������Ă��Ȃ�</returns>
    private bool TargetSearch(Transform target, float angle, float range)
    {
        // �^�[�Q�b�g���ǂ̕����ɋ��邩���v�Z����B
        Vector3 toTarget = (target.position - transform.position).normalized;
        Debug.DrawRay(transform.position, toTarget * range, Color.cyan);

        // �u�����̐��ʂƃ^�[�Q�b�g����������̓��ρv�Ɓu�^�[�Q�b�g�Ƃ̋����v���v�Z����B
        float dot = Vector3.Dot(transform.forward, toTarget);
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // �v�Z�������ςƋ��������͈͓���������A�����������Ƃɂ���B
        if (dot >= angle && distanceToTarget <= range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>�^�[�Q�b�g�Ƃ̊Ԃɏ�Q�������邩�ǂ������ׂ�</summary>
    /// <param name="target"></param>
    /// <param name="range"></param>
    /// <returns>true -> ��Q���i�V | false -> ��Q���A��</returns>
    private bool TargetLook(Transform target, float range)
    {
        // Player���Î�����B
        _turnAroundDir = transform.forward;
        transform.LookAt(target);

        // Ray���΂��Ď��g�ƃ^�[�Q�b�g�̊Ԃɏ�Q�������邩�ǂ����m�F����B
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range);

        // Hit����Collider��Tag��Player�ȊO��������A�Î�����߂�B
        if (hit.collider.tag == "Player")
        {
            return true;
        }
        else
        {
            transform.LookAt(null);
            transform.forward = _turnAroundDir;
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        AddGizmos.DrawWireCone(transform.position, transform.forward, _searchRange, _searchAngle);
    }
}
