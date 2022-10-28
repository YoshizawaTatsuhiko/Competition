using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MapGenerator : MonoBehaviour
{
    /// <summary>����</summary>
    [SerializeField] int _width = 1;
    /// <summary>�c��</summary>
    [SerializeField] int _height = 1;
    RandomMapController _generateMap;

    void Start()
    {
        _generateMap = FindObjectOfType<RandomMapController>();
        _generateMap.GenerateMap(_width, _height);
    }
}
