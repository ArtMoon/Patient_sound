using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensityEffect : MonoBehaviour
{
    [SerializeField] private float _delta = 2;
    [SerializeField] private float _sign = 1;
    [SerializeField] private float _speed = 10;

    [SerializeField] bool _isRandomized = false;

    [SerializeField] private float _minAwaitInterval = 0;
    [SerializeField] private float _maxAwaitInterval = 5;

    [SerializeField] private float _minEffectInterval = 0;
    [SerializeField] private float _maxEffectnterval = 5;


    private Light _light;
    private float _baseIntensity;
    private float _currentAwaitInterval = 0;
    private float _currentEffectInterval = 0;
    private bool _effectStarted = false;


    private void Start()
    {
        _light = GetComponent<Light>();
        _baseIntensity = _light.intensity;

        _currentAwaitInterval = Random.Range(_minAwaitInterval, _maxAwaitInterval);
        
    }

    // Update is called once per frame
    void Update()
    {
       if(!_isRandomized)
       {
            LightEffect();
       }
       else
       {
            if(_currentAwaitInterval  < 0.1f)
            {
                if(!_effectStarted)
                {
                    _currentEffectInterval = Random.Range(_minEffectInterval, _maxEffectnterval);
                    _effectStarted = true;
                }
                
                if (_currentEffectInterval > 0.1f)
                {
                    LightEffect();
                    _currentEffectInterval -= Time.deltaTime;
                }
                else
                {
                    _effectStarted = false;
                    ResetLightIntensity();
                    _currentAwaitInterval = Random.Range(_minAwaitInterval, _maxAwaitInterval);
                }
            }
            else
            {
                _currentAwaitInterval -= Time.deltaTime;
            }
       }
    }

    private void LightEffect()
    {
        _light.intensity = _baseIntensity + (Mathf.Sin(Time.realtimeSinceStartup * _speed) + _sign) *
            _delta / (1 + Mathf.Abs(_sign));
    }

    private void ResetLightIntensity()
    {
        _light.intensity = _baseIntensity;
    }
}
