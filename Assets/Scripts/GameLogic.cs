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
        good_wind,
        good_sun,
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
    public const int YEARS = 7;
    public const int SEASONS = 4;
    public const float MAX_VALUE = 100;
    public const int START_YEAR = 2023;

    // current game timeline
    private int _year;
    private int _season;

    // yearly changes
    [SerializeField] private float _yearlyEnergyChange; 
    [SerializeField] private int _yearlyMoney; 

    // wind conditions
    [SerializeField] private List<int> _windyColumns;
    [SerializeField] private List<int> _windyRows;

    // event probability
    private Event[] _events = {Event.good_wind, Event.good_sun, Event.bad_wind, Event.bad_sun, Event.none, Event.none, Event.none};

    // message handling
    [HideInInspector] public string _message;
    [HideInInspector] public bool _displayMessage;

    // Taxes
    [SerializeField] float _taxMoney;
    [SerializeField] float _taxPeopleHate;

    // values of resources
    private Bar _energy;
    private Bar _biodiversity;
    private Bar _happiness;
    private Bar _money;
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
        _energy._value = MAX_VALUE * 0.25f;
        _biodiversity._value = MAX_VALUE;
        _money._value = MAX_VALUE  * 0.75f;
        _happiness._value = MAX_VALUE;
        _displayMessage = false;

        foreach(KeyValuePair<EnergySource, EnergyLogic> source in _energySources) {
            _energySources[source.Key]._buildAmount = 0;
        }

        ButtonManager.Instance.SetButtonsInteractable(false);
    }

    private void Awake(){
        Instance = this;
        FillDictionaries();   

        _energy = _energyBar.transform.GetChild(0).GetComponent<Bar>();
        _biodiversity = _biodiversityBar.transform.GetChild(0).GetComponent<Bar>();
        _money = _moneyBar.transform.GetChild(0).GetComponent<Bar>();
        _happiness = _happinessBar.transform.GetChild(0).GetComponent<Bar>();
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
    public void EndYear() {
        if(_year < YEARS) {
            _season = 1;
            _year++;
            Debug.Log("Before: " + _energy._value);
            _energy._value = _energy._value * (1.0f - _yearlyEnergyChange);
            Debug.Log("After: " + _energy._value);
            _money._value += _yearlyMoney;

            int index = Random.Range(0, _events.Length);
            switch (_events[index])
            {
                case Event.good_sun:
                    _energy._value = _energy._value * (1.0f + 0.02f * _energySources[EnergySource.photovoltaic]._buildAmount);
                    DisplayMessage("Dieses Jahr war viel Sonnenschein!");
                    break;
                case Event.bad_sun:
                    _energy._value = _energy._value * (1.0f - 0.02f * _energySources[EnergySource.photovoltaic]._buildAmount);
                    DisplayMessage("Dieses Jahr war wenig Sonnenschein!");
                    break;
                case Event.good_wind:
                    _energy._value= _energy._value * (1.0f + 0.02f * _energySources[EnergySource.photovoltaic]._buildAmount);
                    DisplayMessage("Dieses Jahr war viel Wind!");
                    break;
                case Event.bad_wind:
                    _energy._value = _energy._value * (1.0f - 0.02f * _energySources[EnergySource.photovoltaic]._buildAmount);
                    DisplayMessage("Dieses Jahr war wenig Wind!");
                    break;
                default:
                    break;
            }
        } else {
            if(_energy._value >= MAX_VALUE) {
                GameWon();
            } else {
                GameOver(_energy._name);
            }
        }
    }

    // end current season
    private void EndSeason() {
        _displayMessage = false;
        if(_season < SEASONS) {
            _season++;
        } else {
            ButtonManager.Instance.EnableEndYearButton();
        }
    }

    // display message to players
    public void DisplayMessage(string message) {
        _message = message;
        _displayMessage = true;
    }

    // build energy source on tile
    public void BuildOnTile(Tile tile, EnergySource source) {
        string message = tile.Build(source);
        if(message != "") {
            DisplayMessage(message);
        } else {
            _energySources[source]._buildAmount++;
            int cityCount = GridManager.Instance.CheckNeighbours(tile.GetPosition(), Tiletype.city);
            int woodCount = source == EnergySource.windTurbine ? GridManager.Instance.CheckNeighbours(tile.GetPosition(), Tiletype.wood) : 1;
            bool isWindy = false;
            if(source == EnergySource.windTurbine) {

                foreach (int column in _windyColumns) {
                    if(tile.GetPosition().x == column) {
                        isWindy = true;
                    }
                }

                foreach (int row in _windyRows) {
                    if(tile.GetPosition().y == row) {
                        isWindy = true;
                    }
                }
            }
            float energy = isWindy ? _energySources[source]._energy * 2.0f : _energySources[source]._energy;
            int closedGroundFactor = tile.GetTileType() == Tiletype.city || tile.GetTileType() == Tiletype.farm ? 0 : 1;
            int cityFactor = tile.GetTileType() == Tiletype.city ? -1 : 1 * cityCount;
            EndSeason();
            UpdateResources(energy * (1.0f - (_yearlyEnergyChange * _year)),  _energySources[source]._price, _energySources[source]._biodiversity * woodCount * closedGroundFactor, _energySources[source]._happiness * cityFactor);
        }
    }

    // update ressource values
    public void UpdateResources(float energy, float money, float biodiversity, float happiness) {
        _energy._value += energy;
        _biodiversity._value += biodiversity;
        _happiness._value += happiness;
        _money._value += money;

        if(_energy._value <= 0)  {
            GameOver(_energy._name);
        } else if(_biodiversity._value <= 0) {
            GameOver(_biodiversity._name);
        } else if(_happiness._value <= 0 ) {
            GameOver(_happiness._name);
        } else if(_money._value <= 0 ) {
            GameOver(_money._name);
        }
    }

    private void GameOver(string cause) {
        DisplayMessage("Game Over: " + cause);
    }

    private void GameWon() {
        DisplayMessage("Game Won");
    }

    public void CollectTaxes() {
        EndSeason();
        UpdateResources(0, _taxMoney, 0, _taxPeopleHate);
    }
}
