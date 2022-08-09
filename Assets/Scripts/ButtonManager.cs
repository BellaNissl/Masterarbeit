using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance { get; private set; }
   
    private void Awake(){
        Instance = this;
    }

    // make buttons not interactable when selected tile does not support energy type
    public void AdaptIntegrability(Tiletype type){
        Dictionary<EnergySource, EnergyLogic> sources = GameLogic.Instance._energySources;
        foreach(KeyValuePair<EnergySource, EnergyLogic> source in sources) {
            AdaptIntegrability(type, source.Key);
        }
    }

    private void AdaptIntegrability(Tiletype type, EnergySource source) {
        GameLogic.Instance._energySources[source]._button.interactable = GameLogic.Instance._tiles[type]._supportedEnergy.Contains(source);
    }

    // reset all buttons to be interactable
    public void SetButtonsInteractable(bool interactable){
        Dictionary<EnergySource, EnergyLogic> sources = GameLogic.Instance._energySources;
        foreach(KeyValuePair<EnergySource, EnergyLogic> source in sources) {
            sources[source.Key]._button.interactable = interactable;
        }
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
            GameLogic.Instance.BuildOnTile(selected, EnergySource.windTurbine);
            GridManager.Instance.SelectTile(GridManager.INVALID);
        }
    }
}
