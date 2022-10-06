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
    /// <summary>���x�̌v�Z</summary>
    [SerializeField] float _density = 1f;

    void Start()
    {
        _density = 315 / 64 * Mathf.PI * Mathf.Pow(_areaOfInfluence, 9);  //���x�̌v�Z
    }

    /// <summary>���q�̖��x�̌v�Z</summary>
    /// <param name="particle"></param>
    void CalcDencity(ParticleElements[] particle)
    {
        float aoi2 = _areaOfInfluence * _areaOfInfluence;  //_areaOfInfluence ��2����v�Z���Ă���

        for(int i = 0; i < particle.Length; i++)
        {
            var nowParticle = particle[i];
            float sum = 0f;

            for(int j = 0; j < particle.Length; j++)
            {
                if (i == j) continue;

                var nearParticle = particle[j];
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
}
