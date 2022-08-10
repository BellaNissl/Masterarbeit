using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface EnergyLogic
{
    int _buildAmount { get; set; }

    int _price { get; }

    int _energy { get; }

    int _biodiversity { get; }

    int _happiness { get; }

    Button _button { get; }

    Color _color { get; }

    string _name { get; }
}
