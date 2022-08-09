using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface EnergyLogic
{
    int _price { get; }

    int _energy { get; }

    Button _button { get; }

    Color _color { get; }

    string _name { get; }
}
