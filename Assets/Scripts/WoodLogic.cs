using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodLogic : MonoBehaviour, TileLogic
{
    public static WoodLogic Instance { get; private set; }

    [field: SerializeField] public Color _color { get; private set; }

    [field: SerializeField] public bool _photovoltaic_buildable { get; private set; }

    [field: SerializeField] public bool _agrovoltaic_buildable { get; private set; }

    [field: SerializeField] public bool _wind_turbine_buildable { get; private set; }

    [field: SerializeField] public string _photovoltaic_message { get; private set; }

    [field: SerializeField] public string _agrovoltaic_message { get; private set; }

    [field: SerializeField] public string _wind_turbine_message { get; private set; }

    private void Awake(){
        Instance = this;
    }
}
