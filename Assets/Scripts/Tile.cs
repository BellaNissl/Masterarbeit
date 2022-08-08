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

    // energy color
    [SerializeField] private Color _pv_color;
    [SerializeField] private Color _wind_color;

    // logic
    private GameLogic _game;
    private TileLogic _logic;
    private bool _built;

    public Tiletype GetTileType(){
        return _type;
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

    // let the game react to the built type depending on the neighbours and tile type
    public void ReactOnType(Tiletype type){
        Debug.Log(_type + "react on " + type);
        if(_type ==  Tiletype.city) {

        }
        if(_type ==  Tiletype.farm) {
            
        }
        if(_type ==  Tiletype.wood) {
            
        }
        if(_type ==  Tiletype.field) {
            
        }

        _game.UpdateResources(1, 1, 1, 1);
    }

    // build pv generator on tile
    public void BuildPV() {
        if(!CheckIfBuildable()) {
            return;
        }
        if(_logic._pv_buildable) {
            _renderer.color = _pv_color;
            _built = true;
            _game.UpdateResources(20, 10, -10, 0);
        } else {
            _game.DisplayMessage(_logic._pv_message);
        }
    }


    // build wind generator on tile
    public void BuildWind(){
        if(!CheckIfBuildable()) {
            return;
        }
        if(_logic._wind_buildable) {
            _renderer.color = _wind_color;
            _built = true;
            _game.UpdateResources(10, 20, 0, -10);
        } else {
            _game.DisplayMessage(_logic._wind_message);
        }
    }

    public bool CheckIfBuildable(){
        if(_built) {
            _game.DisplayMessage("already built");
            return false;
        }
        return true;
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
