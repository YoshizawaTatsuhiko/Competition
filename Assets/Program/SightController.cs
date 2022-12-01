using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightController : MonoBehaviour
{
    [SerializeField, Tooltip("���G����Ώ�")]
    private Transform _terget;
    [SerializeField, Tooltip("���G�͈�")]
    private float _searchAngle = 1f;
    [SerializeField, Tooltip("���G����")]
    private float _range = 1f;
    /// <summary>�Փ˂����I�u�W�F�N�g�̏��</summary>
    private RaycastHit _hit;

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(_terget.position, transform.position);
        Vector3 vec = _terget.position - transform.position;
        float angle = Vector3.Angle(vec, transform.forward);

        if (angle <= _searchAngle && distance <= _range)
        {
            transform.forward = _terget.position;
            Physics.Raycast(transform.position, vec, out _hit, _range, LayerMask.GetMask(""));
        }
    }
}
