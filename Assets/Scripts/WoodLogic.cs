using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodLogic : MonoBehaviour, TileLogic
{
    public static WoodLogic Instance { get; private set; }

    [field: SerializeField] public Color _color { get; private set; }

    [field: SerializeField] public List<EnergySource> _supportedEnergy { get; private set; }

    [field: SerializeField] public string _name { get; private set; }

    private void Awake(){
        Instance = this;
    }
}
