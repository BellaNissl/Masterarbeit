using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Tiletype _type;
    // [SerializeField] private Tile[] _neighbours;

    // render and selection
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _selection;
    [SerializeField] private GameObject _energyField;

    // logic
    private GameLogic _game;
    private bool _built;
    private EnergySource _buildType;

    public Tiletype GetTileType(){
        return _type;
    }

    public bool GetBuildStatus() {
        return _built;
    }

    public EnergySource GetBuildType(){
        if(_built) {
            return _buildType;
        } 
        return EnergySource.none;
    }

    // initialize tile logic
    public void Init(Tiletype type) {
        _game = GameLogic.Instance;
        _type = type;
        if(_game._tiles[_type]._sprite != null) {
            _renderer.sprite = _game._tiles[_type]._sprite;
        } else {
            _renderer.color = _game._tiles[_type]._color;
        }
    }

    // build on tile
    public string Build(EnergySource source){
        if(_built) {
            return "already built";
        }
        if(_game._tiles[_type]._supportedEnergy.Contains(source)) {
            _buildType = source;
            if(_game._energySources[source]._sprite != null) {
                _energyField.GetComponent<SpriteRenderer>().sprite = _game._energySources[source]._sprite;
            } else {
                _energyField.GetComponent<SpriteRenderer>().color = _game._energySources[source]._color;
            }
            _energyField.SetActive(true);
            _built = true;
            return "";
        } 

        return "cannot build that here";
    }

    public Vector2 GetPosition() {
        return new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y);
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
        GridManager.Instance.SelectTile(GetPosition());
    }

    // deselect the tile
    public void Deselect() {
        _selection.SetActive(false);
    }
}
