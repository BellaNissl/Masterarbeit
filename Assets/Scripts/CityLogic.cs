using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityLogic : MonoBehaviour, TileLogic
{
    public static CityLogic Instance { get; private set; }

    [field: SerializeField] public Color _color { get; set; }

    [field: SerializeField] public bool _pv_buildable { get; set; }

    [field: SerializeField] public bool _wind_buildable { get; set; }

    [field: SerializeField] public string _pv_message { get; set; }

    [field: SerializeField] public string _wind_message { get; set; }

    private void Awake(){
        Instance = this;
    }
}

