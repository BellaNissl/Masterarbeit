using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodLogic : MonoBehaviour, TileLogic
{
    public static WoodLogic Instance { get; private set; }

    [field: SerializeField] public Color _color { get; private set; }

    [field: SerializeField] public Sprite _sprite { get; private set; }

    [field: SerializeField] public List<EnergySource> _supportedEnergy { get; private set; }

    [field: SerializeField] public string _name { get; private set; }

    private void Awake(){
        Instance = this;
    }

    public List<float> _GetEnergyValues(EnergySource source, Vector2 position){
        return new List<float>{0,0,0,0};
    }
}
