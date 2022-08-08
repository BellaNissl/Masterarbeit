using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance { get; private set; }
    [SerializeField] private Button _photovoltaic_button;
    [SerializeField] private Button _agrovoltaic_button;
    [SerializeField] private Button _wind_turbine_button;

    private void Awake(){
        Instance = this;
    }

    // make buttons not interactable when selected tile does not support energy type
    public void AdaptIntegrability(TileLogic logic){
        _photovoltaic_button.interactable = logic._photovoltaic_buildable;
        _agrovoltaic_button.interactable = logic._agrovoltaic_buildable;
        _wind_turbine_button.interactable = logic._wind_turbine_buildable;
    }

    // reset all buttons to be interactable
    public void SetButtonsInteractable(){
        _photovoltaic_button.interactable = _agrovoltaic_button.interactable = _wind_turbine_button.interactable = true;
    }

    public void PhotovoltaicButtonClicked()
    {
        Tile selected = GridManager.Instance.GetSelectedTile();
        if (selected != null) {
            GameLogic.Instance.BuildOnTile(selected, EnergySource.photovoltaic);
            GridManager.Instance.SelectTile(GridManager.INVALID);
        }
    }

    public void AgrovoltaicButtonClicked()
    {
        Tile selected = GridManager.Instance.GetSelectedTile();
        if (selected != null) {
            GameLogic.Instance.BuildOnTile(selected, EnergySource.agrovoltaic);
            GridManager.Instance.SelectTile(GridManager.INVALID);
        }
    }

    public void WindTurbineButtonClicked() {
        Tile selected = GridManager.Instance.GetSelectedTile();
        if (selected != null) {
            GameLogic.Instance.BuildOnTile(selected, EnergySource.wind_turbine);
            GridManager.Instance.SelectTile(GridManager.INVALID);
        }
    }
}
