using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�Q�l����
//https://soysoftware.sakura.ne.jp/archives/1559

/// <summary>���q</summary>
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

class LiquidController : MonoBehaviour
{
    /// <summary>�e���͈�</summary>
    [SerializeField] float _areaOfInfluence = 0f;
    /// <summary>���q�̎���</summary>
    [SerializeField] float _particleMass = 1f;
    /// <summary>���x�̌v�Z�Ŏg��</summary>
    [SerializeField] float _density = 1f;

    void Start()
    {
        _density = 315 / 64 * Mathf.PI * Mathf.Pow(_areaOfInfluence, 9);  //���x�̌v�Z
    }

    //�[�|�|�|�|�|�[���x�Ɋւ��鏈���n�[�|�|�[�|�|�|

    /// <summary>���q�̖��x�̌v�Z</summary>
    /// <param name="particles">���q�̗v�f</param>
    void CalcDencity(ParticleElements[] particles)
    {
        float aoi2 = _areaOfInfluence * _areaOfInfluence;  //_areaOfInfluence ��2����v�Z���Ă���

        for(int i = 0; i < particles.Length; i++)
        {
            var nowParticle = particles[i];
            float sum = 0f;

            for(int j = 0; j < particles.Length; j++)
            {
                if (i == j) continue;  //���肵�Ă���̂��������g��������A�X�L�b�v����

                var nearParticle = particles[j];
                Vector3 particleDistence = nearParticle.position - nowParticle.position;
                float pd2 = Vector3.Dot(particleDistence, particleDistence);

                if(pd2 < aoi2)
                {
                    sum += Mathf.Pow(aoi2 - pd2, 3);
                }
            }
            nowParticle.density = sum * _density;
        }
    }

    //�[�|�|�|�[�|�|���͂Ɋւ��鏈���n�[�|�|�|�[�|�|

    /// <summary>���͌W��</summary>
    [SerializeField] float _pressureCoefficient = 200f;
    /// <summary>�O�͂��������ĂȂ��Ƃ��̖��x</summary>
    [SerializeField] float _restDensity = 1000f;
    [SerializeField] float _pressure = 1f;

    /// <summary>���q�ɂ����鈳�͂��v�Z</summary>
    /// <param name="particles">���q�̗v�f</param>
    void CalcPressure(ParticleElements[] particles)
    {
        float aoi2 = _areaOfInfluence * _areaOfInfluence;

        for(int i = 0; i < particles.Length; i++)
        {
            particles[i].pressure = _pressureCoefficient * (particles[i].density - _restDensity);
        }
        _pressure = 45 / Mathf.PI * Mathf.Pow(_areaOfInfluence, 6);

        for(int i = 0; i < particles.Length; i++)
        {
            var nowParticle = particles[i];
            float sum = 0f;

            for(int j = 0; j < particles.Length; j++)
            {
                if (i == j) continue;
            }
        }
    }
}
