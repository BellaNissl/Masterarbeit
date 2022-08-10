using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance { get; private set; }

    public Button _EndYearButton;
    public Button _taxButton;

    public string _peopleString = "Volksmeinung";
    public string _moneyString = "Finanzen";
    public string _energyString = "Strom";
    public string _biodiversityString = "Biodiversität";
   
    private void Awake(){
        Instance = this;
        _EndYearButton.interactable = false;
    }

    // make buttons not interactable when selected tile does not support energy type
    public void AdaptIntegrability(Tiletype type){
        if(!_EndYearButton.interactable){
            Dictionary<EnergySource, EnergyLogic> sources = GameLogic.Instance._energySources;
            foreach(KeyValuePair<EnergySource, EnergyLogic> source in sources) {
                AdaptIntegrability(type, source.Key);
            }
        }
    }

    private void AdaptIntegrability(Tiletype type, EnergySource source) {
        bool interactable = GameLogic.Instance._tiles[type]._supportedEnergy.Contains(source);

        if(interactable) {
            int cityFactor = 1;
            int bioFactor = 1;

            if(source == EnergySource.windTurbine) {
                bioFactor = GridManager.Instance.CheckNeighbours(GridManager.Instance.GetSelectedTile().GetPosition(), Tiletype.wood);
                cityFactor = GridManager.Instance.CheckNeighbours(GridManager.Instance.GetSelectedTile().GetPosition(), Tiletype.city);
            } else if(source == EnergySource.photovoltaic) {
                if(type == Tiletype.city) {
                    cityFactor = -1;
                    bioFactor = 0;
                } else {
                    cityFactor = GridManager.Instance.CheckNeighbours(GridManager.Instance.GetSelectedTile().GetPosition(), Tiletype.city);
                }
            }

            SetButtonDescription(source, 
                    GameLogic.Instance._energySources[source]._energy, 
                    GameLogic.Instance._energySources[source]._price, 
                    GameLogic.Instance._energySources[source]._happiness * cityFactor, 
                    GameLogic.Instance._energySources[source]._biodiversity * bioFactor);
        } else {
            SetButtonDescriptionEmpty(source);
        }
        GameLogic.Instance._energySources[source]._button.interactable = interactable;
    }

    // reset all buttons to be interactable
    public void SetButtonsInteractable(bool interactable){
        if(!_EndYearButton.interactable){
            Dictionary<EnergySource, EnergyLogic> sources = GameLogic.Instance._energySources;
            foreach(KeyValuePair<EnergySource, EnergyLogic> source in sources) {
                sources[source.Key]._button.interactable = interactable;
            }
        }
        if(!interactable) {
            SetButtonDescriptionEmpty(EnergySource.agrovoltaic);
            SetButtonDescriptionEmpty(EnergySource.windTurbine);
            SetButtonDescriptionEmpty(EnergySource.photovoltaic);
        }
    }

    public void EnableEndYearButton(){
        GridManager.Instance.SelectTile(GridManager.INVALID);
        SetButtonsInteractable(false);
        _EndYearButton.interactable = true;
        _taxButton.interactable = false;
        TextMeshProUGUI description = _taxButton.transform.GetComponentsInChildren<TextMeshProUGUI >()[1];
        description.text = "";
    }

    public void DisableEndYearButton(){
        GridManager.Instance.SelectTile(GridManager.INVALID);
        _EndYearButton.interactable = false;
        _taxButton.interactable = true;
        TextMeshProUGUI description = _taxButton.transform.GetComponentsInChildren<TextMeshProUGUI >()[1];
        description.text = "+ Finanzen\n - Volksmeinung";
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

    public void TaxesButtonClicked() {
        GameLogic.Instance.CollectTaxes();
    }

    public void EndYearButtonClicked() {
        GameLogic.Instance.EndYear();
        DisableEndYearButton();
    }

    public void SetButtonDescriptionEmpty(EnergySource source){
        Button button = GameLogic.Instance._energySources[source]._button;
        TextMeshProUGUI description = button.transform.GetComponentsInChildren<TextMeshProUGUI >()[1];
        description.text = "";
    }

    public void SetButtonDescription(EnergySource source, int energyEffect, int moneyEffect, int peopleEffect, int biodiversityEffect) {
        Button button = GameLogic.Instance._energySources[source]._button;
        string text = "";
        TextMeshProUGUI description = button.transform.GetComponentsInChildren<TextMeshProUGUI >()[1];

        if (energyEffect < 0) {
            text += "- Strom\n";
        } else if(energyEffect > 0) {
            text += "+ Strom\n";
        }

        if (moneyEffect < 0) {
            text += "- Finanzen\n";
        } else if(moneyEffect > 0) {
            text += "+ Finanzen\n";
        }

        if (peopleEffect < 0) {
            text += "- Volksmeinung\n";
        } else if(peopleEffect > 0) {
            text += "+ Volksmeinung\n";
        }

        if (biodiversityEffect < 0) {
            text += "- Biodiversität\n";
        } else if(biodiversityEffect > 0) {
            text += "+ Biodiversität\n";
        }

        description.text = text;
    }
}
