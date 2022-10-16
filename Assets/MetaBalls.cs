using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaBalls : MonoBehaviour
{
    /// <summar>�Ώۂ̃}�e���A��</summary>
    Material _mat = default;
    /// <summary></summary>
    ParticleSystem _particleSystem = default;
    /// <summary></summary>
    ParticleSystem.Particle[] _particles = default;
    /// <summary></summary>
    List<Vector4> _particlesPos;
    /// <summary>�p�[�e�B�N���̑��x</summary>
    float _speed = 1f;

    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _particles = new ParticleSystem.Particle[_particleSystem.main.maxParticles];
        _particlesPos = new List<Vector4>(10);
        _mat = GetComponent<ParticleSystemRenderer>().sharedMaterial;
        _speed = _particleSystem.main.startSpeedMultiplier;
    }

    void Update()
    {
        _particlesPos.Clear();
        int arriveParticle = _particleSystem.GetParticles(_particles);

        for(int i = 0; i < arriveParticle; i++)
        {
            _particlesPos.Add(_particles[i].position);
        }
        _mat.GetVectorArray("PrticlePos", _particlesPos);
    }

    /// <summary>�p�[�e�B�N���̓���(ON/OFF)��؂�ւ���</summary>
    /// <returns></returns>
    public bool TogglePlay()
    {
        if (_particleSystem.isPlaying)
        {
            _particleSystem.Pause();
        }
        else
        {
            _particleSystem.Play();
        }
        return _particleSystem.isPlaying;
    }

    /// <summary>�p�[�e�B�N���̓���(ON/OFF)��؂�ւ���</summary>
    /// <param name="exchange">ON/OFF��؂�ւ���</param>
    public void TogglePlay(bool exchange)
    {
        if(exchange)
        {
            _particleSystem.Play();
        }
        else
        {
            _particleSystem.Pause();
        }
    }

    /// <summary>�p�[�e�B�N���ɗ��C��(�΂��)�𔭐�������</summary>
    /// <returns></returns>
    public bool ToggleNoise()
    {
        ParticleSystem.NoiseModule n = _particleSystem.noise;

        if(n.enabled)
        {
            n.enabled = false;
        }
        else
        {
            n.enabled = true;
        }
        return n.enabled;
    }

    /// <summary></summary>
    /// <param name="exchange"></param>
    public void ToggleNoise(bool exchange)
    {
        ParticleSystem.NoiseModule n = _particleSystem.noise;
        n.enabled = exchange;
    }

    /// <summary>�p�[�e�B�N���̑��x��ω�������</summary>
    /// <param name="newSpeed">���x�X�V�̒l</param>
    public void ChangeSpeed(float newSpeed)
    {
        ParticleSystem.MainModule main = _particleSystem.main;
        main.startSpeedMultiplier = newSpeed * _speed;
    }
}
