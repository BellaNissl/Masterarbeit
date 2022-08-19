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

            if(source == EnergySource.windTurbine) {
                // check if it is windy here
                bool isWindy = false;

                foreach (int column in GameLogic.Instance._windyColumns) {
                    if(position.x == column) {
                        isWindy = true;
                    }
                }

                foreach (int row in GameLogic.Instance._windyRows) {
                    if(position.y == row) {
                        isWindy = true;
                    }
                }

                energy = isWindy ? sources[source]._energy * 2.0f : sources[source]._energy;

                // check how many cities are neighbours
                int cityCount = GridManager.Instance.CountNeighboursOfType(position, Tiletype.city);
                happiness = sources[source]._happiness * cityCount;

                // check how many woods are neighbours
                int woodCount = GridManager.Instance.CountNeighboursOfType(position, Tiletype.wood);
                biodiversity = sources[source]._biodiversity * woodCount;

            } else if(source == EnergySource.photovoltaic) {
                // check how many cities are neighbours
                int cityCount = GridManager.Instance.CountNeighboursOfType(position, Tiletype.city);
                happiness = sources[source]._happiness * cityCount; 
            }
        }

        return new List<float> {energy, money, biodiversity, happiness};
    }
}
