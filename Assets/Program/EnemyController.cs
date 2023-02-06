using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SightController))]

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 1f;
    private Rigidbody _rb = null;
    private SightController _sight = null;
    [SerializeField]
    private float _waitTime = 3f;
    /// <summary>�E���E������Ƃ��Ɍ�������</summary>
    private Vector3 _direction = Vector3.zero;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        _sight = GetComponent<SightController>();
    }

    private float _timer = 0f;
    private bool _isChangeDirection = true;

    private void FixedUpdate()
    {
        if (_sight.SearchTarget())  // �^�[�Q�b�g�𔭌������Ƃ�
        {
            if (!_sight.LookTarget())  // �^�[�Q�b�g�Ƃ̊Ԃɏ�Q�������鎞
            {
                _timer += Time.fixedDeltaTime;
                if (_timer > _waitTime)  // �^�[�Q�b�g����Q���Ɉ�莞�ԉB�ꂽ��A���G���Ȃ����B
                {
                    _timer = 0f;
                    return;
                }
            }
        }
        else  // �^�[�Q�b�g�������ł��Ă��Ȃ��Ƃ�
        {
            if(_isChangeDirection) StartCoroutine(Wander());
        }
        _rb.velocity = transform.forward * _moveSpeed;
    }

    /// <summary>�I�u�W�F�N�g���E���E������</summary>
    private IEnumerator Wander()
    {
        _isChangeDirection = false;
        Vector3 random = Random.insideUnitSphere;
        _direction = new Vector3(random.x, 0f, random.z).normalized;
        transform.forward = _direction;

        yield return new WaitForSeconds(_waitTime);
        _isChangeDirection = true;
    }
}
