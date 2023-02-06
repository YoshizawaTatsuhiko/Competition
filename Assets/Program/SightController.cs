using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightController : MonoBehaviour
{
    [SerializeField]
    private Transform _target = null;
    [SerializeField]
    private float _searchDegree = 30f;
    [SerializeField]
    private float _searchRange = 15f;

    private Vector3 _lineOfSight = Vector3.zero;

    /// <summary>�^�[�Q�b�g�����E���ɓ����Ă��邩���肷��</summary>
    /// <param name="target">���肷��^�[�Q�b�g</param>
    /// <param name="degree">����p(�x���@)</param>
    /// <param name="range">���G�͈�</param>
    /// <returns>true -> ���� | false -> ������ or ��������</returns>
    public bool SearchTarget()
    {
        // �^�[�Q�b�g�̂������
        Vector3 toTarget = _target.position - transform.position;
        // ���g�̐��ʂ� 0���Ƃ��āA180���܂Ŕ��肷��΂����̂ŁAcos(����p/2)�����߂�B
        float cosHalf = Mathf.Cos(_searchDegree / 2 * Mathf.Deg2Rad);
        // ���g�̐��ʂƃ^�[�Q�b�g����������Ƃ�cos�Ƃ̒l���v�Z����B
        float cosAngle = Vector3.Dot(transform.forward, toTarget) / (transform.forward.magnitude * toTarget.magnitude);
        // �^�[�Q�b�g�����E�͈͓��ɓ����Ă��邩�̌��ʂ�Ԃ��B
        return cosAngle >= cosHalf && toTarget.magnitude < _searchRange;
    }

    /// <summary>�^�[�Q�b�g�Ƃ̊Ԃɏ�Q�������邩���肷��</summary>
    /// <param name="target">���肷��^�[�Q�b�g</param>
    /// <param name="range">���G�͈�</param>
    /// <returns>true -> ��Q���i�V | false -> ��Q���A��</returns>
    public bool LookTarget()
    {
        // �^�[�Q�b�g�������������Ɍ�������B
        _lineOfSight = transform.forward;
        // �^�[�Q�b�g���Î�����B
        transform.LookAt(_target);
        // �^�[�Q�b�g�Ƃ̊Ԃ̏�Q�������邩�𒲂ׂ邽�߂�Raycast���΂��B
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _searchRange);

        if (hit.collider.tag == $"{_target.gameObject.tag}")
        {
            return true;
        }
        else
        {
            transform.LookAt(null);
            transform.forward = _lineOfSight;
            return false;
        }
    }
}
