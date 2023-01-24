using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightController : MonoBehaviour
{
    [SerializeField]
    private Transform _target = null;
    [SerializeField, Range(0f, 180f)]
    private float _searchDegree = 30f;
    [SerializeField]
    private float _searchRange = 15f;
    /// <summary>�^�[�Q�b�g�������������Ɍ������</summary>
    private Vector3 _turnAroundDir = Vector3.zero;

    private void Start()
    {
        if (!_target) Debug.LogWarning("�^�[�Q�b�g��assign����Ă��܂���B");
    }

    private void FixedUpdate()
    {
        if (SearchTarget(_target, _searchDegree, _searchRange))
        {
            if (LookTarget(_target, _searchRange))
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

    /// <summary>�^�[�Q�b�g�����E���ɓ����Ă��邩���肷��</summary>
    /// <param name="target">���肷��^�[�Q�b�g</param>
    /// <param name="degree">����p(�x���@)</param>
    /// <param name="range">���G�͈�</param>
    /// <returns>true -> ���� | false -> ������ or ��������</returns>
    private bool SearchTarget(Transform target, float degree, float range)
    {
        // �^�[�Q�b�g�̂������
        Vector3 toTarget = target.position - transform.position;
        // ���g�̐��ʂ� 0���Ƃ��āA180���܂Ŕ��肷��΂����̂ŁAcos(����p/2)�����߂�B
        float cosHalf = Mathf.Cos(degree / 2 * Mathf.Deg2Rad);
        // ���g�̐��ʂƃ^�[�Q�b�g����������Ƃ�cos�Ƃ̒l���v�Z����B
        float cosAngle = Vector3.Dot(transform.forward, toTarget) / (transform.forward.magnitude * toTarget.magnitude);
        // �^�[�Q�b�g�����E�͈͓��ɓ����Ă��邩�̌��ʂ�Ԃ��B
        return cosAngle >= cosHalf && toTarget.magnitude < range;
    }

    /// <summary>�^�[�Q�b�g�Ƃ̊Ԃɏ�Q�������邩���肷��</summary>
    /// <param name="target">���肷��^�[�Q�b�g</param>
    /// <param name="range">���G�͈�</param>
    /// <returns>true -> ��Q���i�V | false -> ��Q���A��</returns>
    private bool LookTarget(Transform target, float range)
    {
        // �^�[�Q�b�g�������������Ɍ�������B
        _turnAroundDir = transform.forward;
        // �^�[�Q�b�g���Î�����B
        transform.LookAt(target);
        // �^�[�Q�b�g�Ƃ̊Ԃ̏�Q�������邩�𒲂ׂ邽�߂�Raycast���΂��B
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range);

        if (hit.collider.tag == $"{target.gameObject.tag}")
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * _searchRange);
    }
}
