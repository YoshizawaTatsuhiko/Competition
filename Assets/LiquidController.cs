using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�Q�l����
//https://soysoftware.sakura.ne.jp/archives/1559

class ParticleElements
{
    /// <summary>���W</summary>
    public Vector3 position;
    /// <summary>���x</summary>
    public Vector3 velocity;
    /// <summary>��</summary>
    public Vector3 force;
    /// <summary>���x</summary>
    public float density;
    /// <summary>����</summary>
    public float pressure;
}

public class LiquidController : MonoBehaviour
{
    /// <summary>�e���͈�</summary>
    [SerializeField] float _areaOfInfluence = 0f;
    /// <summary>���q�̎���</summary>
    [SerializeField] float _particleMass = 1f;
    /// <summary>���x</summary>
    [SerializeField] float _density = 1f;
    void Start()
    {
        _density = 315 / 64 * Mathf.PI * Mathf.Pow(_areaOfInfluence, 9);  //���x�̌v�Z
    }

    void Update()
    {
        
    }
}
