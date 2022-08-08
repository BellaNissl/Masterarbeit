using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private Tiletype _type;
    // [SerializeField] private Tile[] _neighbours;

    // render and selection
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _selection;

    // energy colors
    [SerializeField] private Color _photovoltaic_color;
    [SerializeField] private Color _agrovoltaic_color;
    [SerializeField] private Color _wind_turbine_color;

    // logic
    private GameLogic _game;
    private TileLogic _logic;
    private bool _built;

    public Tiletype GetTileType(){
        return _type;
    }

    public TileLogic GetTileLogic() {
        return _logic;
    }

    // initialize tile logic
    public void Init(Tiletype type) {
        _game = GameLogic.Instance;
        _type = type;

        switch (_type)
        {
            case Tiletype.city:
                _logic = CityLogic.Instance; 
                break;
            case Tiletype.wood:
                _logic = WoodLogic.Instance; 
                break;
            case Tiletype.farm:
                _logic = FarmLogic.Instance; 
                break;
            case Tiletype.field:
                _logic = FieldLogic.Instance; 
                break;
            default:
            Debug.Log("No type set!!!");
            break;
        }
        _renderer.color = _logic._color;
    }

    // build on tile
    public string Build(EnergySource source){
        if(_built) {
            return "already built";
        }

        switch(source) 
        {
            case EnergySource.photovoltaic:
                if(_logic._photovoltaic_buildable) {
                    _renderer.color = _photovoltaic_color;
                    _built = true; 
                    return "";
                } else {
                    return _logic._photovoltaic_message;
                }
            case EnergySource.agrovoltaic:
                if(_logic._agrovoltaic_buildable) {
                    _renderer.color = _agrovoltaic_color;
                    _built = true; 
                    return "";
                } else {
                    return _logic._agrovoltaic_message;
                }
            case EnergySource.wind_turbine:
                if(_logic._wind_turbine_buildable) {
                    _renderer.color = _wind_turbine_color;
                    _built = true; 
                    return "";
                } else {
                    return _logic._wind_turbine_message;
                }
            default:
                Debug.Log("source type not valid!!");
                return "";
        }
    }

    // highlight when hovering
    private void OnMouseEnter(){
        _highlight.SetActive(true);
    }

    // stop highlighting when leaving
    private void OnMouseExit() {
        _highlight.SetActive(false);
    }

    // select the tile on mouse down
    private void OnMouseDown(){
        _selection.SetActive(true);
        GridManager.Instance.SelectTile(new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y));
    }

    // deselect the tile
    public void Deselect() {
        _selection.SetActive(false);
    }
}
