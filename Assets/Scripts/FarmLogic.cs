using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmLogic : MonoBehaviour, TileLogic
{
    public static FarmLogic Instance { get; private set; }

    [field: SerializeField] public Color _color { get; private set; }

    [field: SerializeField] public Sprite _sprite { get; private set; }

    [field: SerializeField] public List<EnergySource> _supportedEnergy { get; private set; }

    [field: SerializeField] public string _name { get; private set; }

    private void Awake(){
        Instance = this;
    }

    public List<float> _GetEnergyValues(EnergySource source, Vector2 position){
        float energy = 0;
        float money = 0;
        float biodiversity = 0;
        float happiness = 0;
        
        if(_supportedEnergy.Contains(source)) {
            Dictionary<EnergySource, EnergyLogic> sources = GameLogic.Instance._energySources;
            money = sources[source]._price;
            energy = sources[source]._energy;
            biodiversity = sources[source]._biodiversity;
            happiness = sources[source]._happiness;
        }

        return new List<float> {energy, money, biodiversity, happiness};
    }
}
