using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public string _name;
    private const float MAX_VALUE = 100.0f;
    private float _value = 0.0f;
    public float _animationTime;
    private float _previousValue;

    private Image _bar;

    public void SetValue(float value) {
        _previousValue = _value;
        _value = value;
    }

    public void SetInitialValue(float value) {
        _previousValue = value;
        _value = value;
        _bar.fillAmount = _value / MAX_VALUE;
    }


    public float GetValue() {
        return _value;
    }

    void Awake() {
        _bar = GetComponent<Image>();
    }

    void Update()
    {
        float speed = Mathf.Abs(_value / MAX_VALUE - _previousValue / MAX_VALUE) / _animationTime;

        if(_previousValue < _value) {
            if(_bar.fillAmount < _value / MAX_VALUE) {
                _bar.fillAmount += speed * Time.deltaTime;
            } else {
                _bar.fillAmount = _value / MAX_VALUE;
            }
        } else {
            if(_bar.fillAmount > _value / MAX_VALUE) {
                _bar.fillAmount -=  speed * Time.deltaTime;
            } else {
                _bar.fillAmount = _value / MAX_VALUE;
            }
        }
       
    }
}
