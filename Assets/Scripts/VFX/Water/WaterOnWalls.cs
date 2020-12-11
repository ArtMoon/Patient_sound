using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterOnWalls : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    [SerializeField] private float _waveSpeedY = 1;
    [SerializeField] private float _waveSpeedX = 1;
    private float _startYOffset = 0;
    private float _startXOffset = 0;


    private void Update()
    {
        if (_materials == null) return;
        if (Mathf.Abs(_startYOffset) == 1000) _startYOffset = 0;
        if (Mathf.Abs(_startXOffset) == 1000) _startYOffset = 0;

        foreach (var mat in _materials)
        {
            mat.SetTextureOffset("_DetailAlbedoMap", new Vector2(_startXOffset, _startYOffset));
        }
        _startYOffset += _waveSpeedY * Time.deltaTime;
        _startXOffset += _waveSpeedX * Time.deltaTime;

    }
}
