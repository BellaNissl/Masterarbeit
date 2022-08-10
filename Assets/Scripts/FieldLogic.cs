using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldLogic : MonoBehaviour, TileLogic
{
    public static FieldLogic Instance { get; private set; }

    [field: SerializeField] public Color _color { get; private set; }

    [field: SerializeField] public Sprite _sprite { get; private set; }

    [field: SerializeField] public List<EnergySource> _supportedEnergy { get; private set; }

    [field: SerializeField] public string _name { get; private set; }

    private void Awake(){
        Instance = this;
    }
}
