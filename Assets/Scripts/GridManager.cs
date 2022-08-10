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
    [SerializeField] private Tile _tilePrefab;
    private Dictionary<Vector2, Tile> _tiles; 
    private Tiletype[] occurances = {Tiletype.city, Tiletype.farm, Tiletype.wood, Tiletype.wood, Tiletype.wood, Tiletype.field, Tiletype.field, Tiletype.field, Tiletype.field, Tiletype.field};

    // currently selected tile
    private Vector2 _selectedPos;

    private void Awake(){
        Instance = this;
    }

    void Start() {
        GenerateGrid();
        _selectedPos = INVALID;
    }

    // generate grid and spawn tiles 
    void GenerateGrid() {
        _tiles = new Dictionary<Vector2, Tile>();
        for(int x = 0; x < _width; x++) {
            for(int y = 0; y < _height; y++) {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                int index = Random.Range(0, occurances.Length);
                spawnedTile.Init(occurances[index]);
                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }
    }

    // check tile neighbours for specific types
    public int CheckNeighbours(Vector2 currentPos, Tiletype type){
        int count = 0;
        for(int x = (int)currentPos.x - 1; x <= currentPos.x + 1; x++) {
            for(int y = (int)currentPos.y - 1; y <= currentPos.y + 1; y++) {
                if(x != currentPos.x || y != currentPos.y) {
                    Tile neighbour = getTileAtPosition(new Vector2(x, y));
                    if(neighbour != null && neighbour.GetTileType() == type) {
                        count++;
                    }
                }

            }
        }
        return count;
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
        if(_selectedPos != null && _selectedPos != INVALID) {
            getTileAtPosition(_selectedPos).Deselect();
        }
        _selectedPos = pos;
        if(_selectedPos != null && _selectedPos != INVALID) {
            if(getTileAtPosition(_selectedPos).GetBuildStatus()) {
                ButtonManager.Instance.SetButtonsInteractable(false);
            } else {
                ButtonManager.Instance.AdaptIntegrability(getTileAtPosition(_selectedPos).GetTileType());
                //SetButtonStati(getTileAtPosition(_selectedPos).GetTileType());
            }
        } else {
            ButtonManager.Instance.SetButtonsInteractable(true);
        }
    }

    // return the currently selected tile
    public Tile GetSelectedTile(){
        return getTileAtPosition(_selectedPos);
    }

    // set button infos according to the selected tile
    // private void SetButtonStati(Tiletype type){   
    //     Dictionary<EnergySource, EnergyLogic> sources = GameLogic.Instance._energySources;
    //     switch(type) {
    //         case Tiletype.farm:
    //             ButtonManager.Instance.SetButtonDescription(EnergySource.agrovoltaic, 
    //             sources[EnergySource.agrovoltaic]._energy, 
    //             sources[EnergySource.agrovoltaic]._price, 
    //             sources[EnergySource.agrovoltaic]._happiness, 
    //             sources[EnergySource.agrovoltaic]._biodiversity);
    //             ButtonManager.Instance.SetButtonDescriptionEmpty(EnergySource.photovoltaic);
    //             ButtonManager.Instance.SetButtonDescriptionEmpty(EnergySource.windTurbine);
    //             break;
    //         case Tiletype.city:
    //             ButtonManager.Instance.SetButtonDescription(EnergySource.photovoltaic, 
    //             sources[EnergySource.photovoltaic]._energy, 
    //             sources[EnergySource.photovoltaic]._price, 
    //             sources[EnergySource.photovoltaic]._happiness * (-1), 
    //             sources[EnergySource.photovoltaic]._biodiversity);
    //             ButtonManager.Instance.SetButtonDescriptionEmpty(EnergySource.agrovoltaic);
    //             ButtonManager.Instance.SetButtonDescriptionEmpty(EnergySource.windTurbine);
    //             break;
    //         case Tiletype.field:
    //             int cityCount = GridManager.Instance.CheckNeighbours(GetSelectedTile().GetPosition(), Tiletype.city);
    //             int woodCount = GridManager.Instance.CheckNeighbours(GetSelectedTile().GetPosition(), Tiletype.wood);

    //             ButtonManager.Instance.SetButtonDescription(EnergySource.photovoltaic, 
    //             sources[EnergySource.photovoltaic]._energy, 
    //             sources[EnergySource.photovoltaic]._price, 
    //             sources[EnergySource.photovoltaic]._happiness * cityCount, 
    //             sources[EnergySource.photovoltaic]._biodiversity);

    //             ButtonManager.Instance.SetButtonDescription(EnergySource.windTurbine, 
    //             sources[EnergySource.windTurbine]._energy, 
    //             sources[EnergySource.windTurbine]._price, 
    //             sources[EnergySource.windTurbine]._happiness * cityCount, 
    //             sources[EnergySource.windTurbine]._biodiversity * woodCount);

    //             ButtonManager.Instance.SetButtonDescriptionEmpty(EnergySource.agrovoltaic);

    //             break;
    //         default:
    //             break;
    //     }

    // }
}
