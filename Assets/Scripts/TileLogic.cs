using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TileLogic
{
    Color _color { get; }

    bool _photovoltaic_buildable { get; }

    bool _agrovoltaic_buildable { get; }

    bool _wind_turbine_buildable { get; }

    string _photovoltaic_message { get; }

    string _agrovoltaic_message { get; }

    string _wind_turbine_message { get; }
}
