using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotovoltaicLogic : MonoBehaviour, EnergyLogic
{
    public static PhotovoltaicLogic Instance { get; private set; }

    [field: SerializeField] public int _price { get; private set; }

    [field: SerializeField] public int _energy { get; private set; }

    [field: SerializeField] public Button _button { get; private set; }

    [field: SerializeField] public Color _color { get; private set; }

    [field: SerializeField] public string _name { get; private set; }

    private void Awake(){
        Instance = this;
    }
}
