using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }
    public static readonly Vector2 INVALID = new Vector2(100000, 100000);
    // grid definition
    [SerializeField] private int _width;
    [SerializeField] private int _height;

    // tile generation 
    [SerializeField] private Tile _tile_prefab;
    private Dictionary<Vector2, Tile> _tiles; 

    // currently selected tile
    private Vector2 _selected_pos;

    private void Awake(){
        Instance = this;
    }

    void Start() {
        GenerateGrid();
    }

    // generate grid and spawn tiles 
    void GenerateGrid() {
        _tiles = new Dictionary<Vector2, Tile>();
        for(int x = 0; x < _width; x++) {
            for(int y = 0; y < _height; y++) {
                var spawned_tile = Instantiate(_tile_prefab, new Vector3(x, y), Quaternion.identity);
                spawned_tile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0) ;
                spawned_tile.Init(Tiletype.field);
                _tiles[new Vector2(x, y)] = spawned_tile;
            }
        }
    }

    // check tile neighbours for specific types
    public List<Tile> GetNeighbours(Vector2 current_pos){
        List<Tile> neighbours = new List<Tile>();
        for(int x = -1; x <= current_pos.x + 1; x++) {
            for(int y = -1; y <= current_pos.y + 1; y++) {
                if(current_pos.x != x && current_pos.y != y) {
                    Tile neighbour = getTileAtPosition(new Vector2(x, y));
                    if(neighbour != null) {
                        neighbours.Add(neighbour);
                    }
                }
            }
        }
        return neighbours;
    }

    // return tile at given position
    public Tile getTileAtPosition(Vector2 pos) {
        if(_tiles.TryGetValue(pos, out var tile)) {
            return tile;
        }

        return null;
    }

    // select tile at position and deselect last selection
    // set button integrability accordingly
    public void SelectTile(Vector2 pos){
        if(_selected_pos != null && _selected_pos != INVALID) {
            getTileAtPosition(_selected_pos).Deselect();
        }
        _selected_pos = pos;
        if(_selected_pos != null && _selected_pos != INVALID) {
            ButtonManager.Instance.AdaptIntegrability(getTileAtPosition(_selected_pos).GetTileLogic());
        } else {
            ButtonManager.Instance.SetButtonsInteractable();
        }
    }

    // return the currently selected tile
    public Tile GetSelectedTile(){
        return getTileAtPosition(_selected_pos);
    }
}
