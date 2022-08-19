using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TileLogic
{
    Color _color { get; }

    List<EnergySource> _supportedEnergy { get; }

    Sprite _sprite { get; }

    string _name { get; }

    List<float> _GetEnergyValues(EnergySource source, Vector2 position);
}
