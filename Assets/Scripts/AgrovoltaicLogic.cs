using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgrovoltaicLogic : MonoBehaviour, EnergyLogic
{
    public static AgrovoltaicLogic Instance { get; private set; }

    public int _buildAmount { get; set; }

    [field: SerializeField] public int _price { get; private set; }

    [field: SerializeField] public int _energy { get; private set; }

    [field: SerializeField] public int _biodiversity { get; private set; }

    [field: SerializeField] public int _happiness { get; private set; }

    [field: SerializeField] public Button _button { get; private set; }

    [field: SerializeField] public Color _color { get; private set; }

    [field: SerializeField] public string _name { get; private set; }

    private void Awake(){
        Instance = this;
    }
}
