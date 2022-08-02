using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    private const float MAX_VALUE = 100.0f;
    public float _value;

    private Image _bar;
    // Start is called before the first frame update
    void Start()
    {
        _bar = GetComponent<Image>();
        _value = MAX_VALUE / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        _bar.fillAmount = _value / MAX_VALUE;
    }
}
