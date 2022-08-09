using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManager : MonoBehaviour
{
    [SerializeField] private GameObject _year;
    [SerializeField] private GameObject _season;
    [SerializeField] private GameObject _tileType;
    [SerializeField] private GameObject _tileInfo;
    [SerializeField] private GameObject _message;

    void Update(){
        _year.GetComponent<TextMeshProUGUI>().text = (2020 + GameLogic.Instance.GetYear()).ToString();
        _season.GetComponent<TextMeshProUGUI>().text = GameLogic.Instance.GetSeason().ToString()  + ". Quartal";

        Tile selected = GridManager.Instance.GetSelectedTile();
        if(selected != null) {
            TileLogic logic = GameLogic.Instance._tiles[selected.GetTileType()];
            _tileType.GetComponent<TextMeshProUGUI>().text = logic._name;
            _tileInfo.GetComponent<TextMeshProUGUI>().text = "Baustatus: ";
            _tileInfo.GetComponent<TextMeshProUGUI>().text += selected.GetBuildStatus() ? GameLogic.Instance._energySources[selected.GetBuildType()]._name : "Leer";
        } else {
            _tileType.GetComponent<TextMeshProUGUI>().text = "";
            _tileInfo.GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void DisplayMessage(string message) {

    }
}
