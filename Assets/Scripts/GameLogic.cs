using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public enum Tiletype
    {
        city,
        farm, 
        wood, 
        field
    };

    public enum Event
    {
        wind,
        sun,
        bad_wind,
        bad_sun,
        none
    };

    public enum EnergySource {
        photovoltaic,
        agrovoltaic,
        windTurbine,
        none
    }

public class GameLogic : MonoBehaviour
{
    public static GameLogic Instance { get; private set; }

    // game constants
    public const int YEARS = 10;
    public const int SEASONS = 4;
    public const int MAX_VALUE = 100;


    // current game timeline
    private int _year;
    private int _season;

    // values of resources
    private int _energy;
    private int _biodiversity;
    private int _happiness;
    private int _money;
    [SerializeField] private GameObject _energyBar;
    [SerializeField] private GameObject _biodiversityBar;
    [SerializeField] private GameObject _happinessBar;
    [SerializeField] private GameObject _moneyBar;

    [HideInInspector] public Dictionary<EnergySource, EnergyLogic> _energySources  { get; private set; }
    [HideInInspector] public Dictionary<Tiletype, TileLogic> _tiles  { get; private set; }

    public int GetSeason() {
        return _season;
    }

    public int GetYear() {
        return _year;
    }

    // set ressource values at start
    private void Start() {
        _year = 0;
        _season = 1;
        _energy = MAX_VALUE / 2;
        _biodiversity = MAX_VALUE;
        _money = MAX_VALUE;
        _happiness = MAX_VALUE;
        UpdateResources(0, 0, 0, 0);
    }

    private void Awake(){
        Instance = this;
        FillDictionaries();   
    }

    private void FillDictionaries() {
        _energySources = new Dictionary<EnergySource, EnergyLogic>();
        _energySources[EnergySource.photovoltaic] = PhotovoltaicLogic.Instance;
        _energySources[EnergySource.agrovoltaic] = AgrovoltaicLogic.Instance;
        _energySources[EnergySource.windTurbine] = WindTurbineLogic.Instance;

        _tiles = new Dictionary<Tiletype, TileLogic>();
        _tiles[Tiletype.city] = CityLogic.Instance;
        _tiles[Tiletype.farm] = FarmLogic.Instance;
        _tiles[Tiletype.wood] = WoodLogic.Instance;
        _tiles[Tiletype.field] = FieldLogic.Instance;
    }

    // end current year
    private void EndYear() {
        if(_year < YEARS) {
            _season = 1;
            _year++;
        } else {
            // end game
        }
    }

    // end current season
    private void EndSeason() {
        if(_season < SEASONS) {
            _season++;
        } else {
            EndYear();
        }
    }

    // display message to players
    public void DisplayMessage(string message) {

    }

    // build energy source on tile
    public void BuildOnTile(Tile tile, EnergySource source) {
        string message = tile.Build(source);
        if(message != "") {
            DisplayMessage(message);
        } else {
            UpdateResources(0, 0, 0, -_energySources[source]._price);
            EndSeason();
        }
    }

    // update ressource values
    public void UpdateResources(int energy, int biodiversity, int happiness, int money) {
        _energy += energy;
        _biodiversity += biodiversity;
        _happiness += happiness;
        _money += money;
        UpdateBars();

        if(_energy <= 0 || _biodiversity <= 0 || _happiness <= 0 || _money <= 0 ) {
            GameOver();
        }
    }

    // update ressource bars
    private void UpdateBars()
    {
        _energyBar.GetComponent<Bar>().SetValue(_energy);
        _biodiversityBar.GetComponent<Bar>().SetValue(_biodiversity);
        _happinessBar.GetComponent<Bar>().SetValue(_happiness);
        _moneyBar.GetComponent<Bar>().SetValue(_money);
    }

    private void GameOver() {
        Debug.Log("Game Over");
    }
}
