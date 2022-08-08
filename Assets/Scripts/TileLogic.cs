using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TileLogic
{
    Color _color { get; set; }

    bool _pv_buildable { get; set; }

    bool _wind_buildable { get; set; }

    string _pv_message { get; set; }

    string _wind_message { get; set; }
}
