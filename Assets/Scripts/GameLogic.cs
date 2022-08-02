using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public static GameLogic Instance { get; private set; }
    public Button _pvButton;
    public Button _windButton;

    private void Awake()
    {
        Instance = this;
    }
}
