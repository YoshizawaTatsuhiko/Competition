using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class SightController : MonoBehaviour
{
    /// <summary>���G�p��SphereCollider</summary>
    private SphereCollider _sphere = default;

    void Start()
    {
        _sphere = GetComponent<SphereCollider>();
        _sphere.isTrigger = true;
    }

    void Update()
    {
        
    }
}
